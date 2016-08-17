using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Plugins.DownloadCache;

namespace IntranetMobile.Droid.Views.Controls
{
    [Obsolete("Use propriate MvxDynamicCompressedBitmapHelper instead", true)]
    public class CompressedMvxAppCompatImageView : MvxAppCompatImageView
    {
        private readonly object _mutex = new object();
        private CancellationTokenSource _cancellationSource;
        private string _imageUrlToCompress;

        public CompressedMvxAppCompatImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CompressedMvxAppCompatImageView(Context context) : base(context)
        {
        }

        public CompressedMvxAppCompatImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public string ImageUrlToCompress
        {
            get { return _imageUrlToCompress; }
            set
            {
                lock (_mutex)
                {
                    _imageUrlToCompress = value;

                    if (_cancellationSource != null)
                    {
                        _cancellationSource.Cancel();
                        _cancellationSource = null;
                    }

                    var cancelTokenSource = new CancellationTokenSource();
                    var cancelToken = cancelTokenSource.Token;
                    _cancellationSource = cancelTokenSource;

                    var dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher>();
                    dispatcher.RequestMainThreadAction(() => { SetImageDrawable(null); });

                    if (string.IsNullOrEmpty(_imageUrlToCompress))
                    {
                        return;
                    }

                    Task.Factory.StartNew(async delegate
                    {
                        var cache = Mvx.Resolve<IMvxImageCache<Bitmap>>();
                        var bitmap = await cache.RequestImage(_imageUrlToCompress);

                        var maxSize = 400;
                        int outWidth;
                        int outHeight;
                        var inWidth = bitmap.Width;
                        var inHeight = bitmap.Height;
                        if (inWidth > inHeight)
                        {
                            outWidth = maxSize;
                            outHeight = inHeight*maxSize/inWidth;
                        }
                        else
                        {
                            outHeight = maxSize;
                            outWidth = inWidth*maxSize/inHeight;
                        }

                        if (cancelToken.IsCancellationRequested)
                        {
                            return;
                        }

                        var resizedBitmap = Bitmap.CreateScaledBitmap(bitmap, outWidth,
                            outHeight,
                            false);

                        if (cancelToken.IsCancellationRequested)
                        {
                            return;
                        }

                        var bitmapDrawable = new BitmapDrawable(resizedBitmap);

                        // TODO: Find out bound handling
                        bitmapDrawable.SetBounds(0, 0, outWidth, outHeight);

                        if (cancelToken.IsCancellationRequested)
                        {
                            return;
                        }

                        dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher>();
                        dispatcher.RequestMainThreadAction(() =>
                        {
                            lock (_mutex)
                            {
                                if (cancelToken.IsCancellationRequested)
                                {
                                    return;
                                }
                                SetImageDrawable(bitmapDrawable);
                            }
                        });
                    }, cancelToken);
                }
            }
        }
    }
}