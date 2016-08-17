using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.DownloadCache;

namespace IntranetMobile.Droid.Views.Util
{
    public class MvxDynamicCompressedBitmapHelper
        : IMvxImageHelper<Bitmap>
    {
        #region ImageState enum

        public enum ImageState
        {
            DefaultShown,
            ErrorShown,
            HttpImageShown
        }

        #endregion ImageState enum

        // TODO: Dynamically bind dat
        private const int MaxSize = 800;
        private readonly object _mutex = new object();

        private CancellationTokenSource _cancellationSource;

        private ImageState _currentImageState = ImageState.DefaultShown;

        private string _defaultImagePath;

        private string _errorImagePath;

        private string _imageUrl;

        public string DefaultImagePath
        {
            get { return _defaultImagePath; }
            set
            {
                if (_defaultImagePath == value)
                    return;
                _defaultImagePath = value;
                OnImagePathChanged().ConfigureAwait(false);

                if (string.IsNullOrEmpty(_errorImagePath))
                    ErrorImagePath = value;
            }
        }

        public string ErrorImagePath
        {
            get { return _errorImagePath; }
            set
            {
                if (_errorImagePath == value)
                    return;
                _errorImagePath = value;
                OnImagePathChanged().ConfigureAwait(false);
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl == value)
                    return;
                _imageUrl = value;
                RequestImage(_imageUrl);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members

        public event EventHandler<MvxValueEventArgs<Bitmap>> ImageChanged;

        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }

        ~MvxDynamicCompressedBitmapHelper()
        {
            Dispose(false);
        }

        private void FireImageChanged(Bitmap image)
        {
            var handler = ImageChanged;
            handler?.Invoke(this, new MvxValueEventArgs<Bitmap>(image));
        }

        private void RequestImage(string imageSource)
        {
            lock (_mutex)
            {
                if (_cancellationSource != null)
                {
                    _cancellationSource.Cancel();
                    _cancellationSource = null;
                }

                var cancelTokenSource = new CancellationTokenSource();
                var cancelToken = cancelTokenSource.Token;
                _cancellationSource = cancelTokenSource;

                Task.Factory.StartNew(async delegate
                {
                    // Critical check
                    lock (_mutex)
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            return;
                        }
                        FireImageChanged(null);
                    }

                    if (string.IsNullOrEmpty(imageSource))
                    {                    
                        // Critical check
                        lock (_mutex)
                        {
                            if (cancelToken.IsCancellationRequested)
                            {
                                return;
                            }
                            ShowDefaultImage().Wait(cancelToken);
                        }
                        return;
                    }

                    if (imageSource.ToUpper().StartsWith("HTTP"))
                    {
                        // Critical check
                        lock (_mutex)
                        {
                            if (cancelToken.IsCancellationRequested)
                            {
                                return;
                            }
                            NewHttpImageRequestedAsync().Wait(cancelToken);
                        }

                        var error = false;
                        try
                        {
                            var cache = Mvx.Resolve<IMvxImageCache<Bitmap>>();
                            var image = await cache.RequestImage(imageSource).ConfigureAwait(false);

                            if (image == null)
                            {
                                // Critical check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    ShowErrorImage().Wait(cancelToken);
                                }
                            }
                            else
                            {
                                // Performance-friendly check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                }

                                int outWidth;
                                int outHeight;
                                var inWidth = image.Width;
                                var inHeight = image.Height;
                                if (inWidth > inHeight)
                                {
                                    outWidth = MaxSize;
                                    outHeight = inHeight*MaxSize/inWidth;
                                }
                                else
                                {
                                    outHeight = MaxSize;
                                    outWidth = inWidth*MaxSize/inHeight;
                                }

                                // Performance-friendly check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                }

                                var resizedBitmap = Bitmap.CreateScaledBitmap(image, outWidth,
                                    outHeight,
                                    false);

                                // Critical check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    NewImageAvailable(resizedBitmap);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Mvx.Trace("failed to download image {0} : {1}", imageSource, ex.ToLongString());
                            error = true;
                        }

                        if (error)
                        {
                            // Critical check
                            lock (_mutex)
                            {
                                if (cancelToken.IsCancellationRequested)
                                {
                                    return;
                                }
                                HttpImageErrorSeenAsync().Wait(cancelToken);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            var image = await ImageFromLocalFileAsync(imageSource).ConfigureAwait(false);

                            if (image == null)
                            {
                                // Critical check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    ShowErrorImage().Wait(cancelToken);
                                }
                            }
                            else
                            {
                                // Critical check
                                lock (_mutex)
                                {
                                    if (cancelToken.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    NewImageAvailable(image);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Mvx.Error(ex.Message);
                        }
                    }
                }, cancelToken);
            }
        }

        private Task OnImagePathChanged()
        {
            switch (_currentImageState)
            {
                case ImageState.ErrorShown:
                    return ShowErrorImage();

                default:
                    return ShowDefaultImage();
            }
        }

        private Task ShowDefaultImage()
        {
            return ShowLocalFileAsync(_defaultImagePath);
        }

        private Task ShowErrorImage()
        {
            return ShowLocalFileAsync(_errorImagePath);
        }

        private async Task ShowLocalFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                FireImageChanged(null);
            }
            else
            {
                FireImageChanged(null);

                try
                {
                    var localImage = await ImageFromLocalFileAsync(filePath).ConfigureAwait(false);
                    if (localImage == null)
                        MvxTrace.Warning("Failed to load local image for filePath {0}", filePath);

                    FireImageChanged(localImage);
                }
                catch (Exception ex)
                {
                    Mvx.Error(ex.Message);
                }
            }
        }

        private async Task<Bitmap> ImageFromLocalFileAsync(string path)
        {
            var loader = Mvx.Resolve<IMvxLocalFileImageLoader<Bitmap>>();
            var img = await loader.Load(path, true, MaxWidth, MaxHeight).ConfigureAwait(false);
            return img.RawImage;
        }

        private Task NewHttpImageRequestedAsync()
        {
            _currentImageState = ImageState.DefaultShown;
            return ShowDefaultImage();
        }

        private Task HttpImageErrorSeenAsync()
        {
            _currentImageState = ImageState.ErrorShown;
            return ShowErrorImage();
        }

        private void NewImageAvailable(Bitmap image)
        {
            _currentImageState = ImageState.HttpImageShown;
            FireImageChanged(image);
        }

        protected virtual void Dispose(bool isDisposing)
        {
        }
    }
}