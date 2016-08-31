using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using IntranetMobile.Core.Services;
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
        private const string _picCacheDir = "_Caches/Pictures/";

        private const int MaxMemoryCacheSize = 40;

        private const int CacheFileExpirationDays = 3;

        #region ImageState enum

        public enum ImageState
        {
            DefaultShown,
            ErrorShown,
            HttpImageShown
        }

        #endregion ImageState enum

        private static ConcurrentDictionary<string, Bitmap> _localFilesCache;

        private static ConcurrentDictionary<string, CacheItem> _remoteFilesCache;

        private static bool IsCacheInitialized;

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
                RequestImageAsync(_imageUrl).ConfigureAwait(false);
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

        public MvxDynamicCompressedBitmapHelper()
        {
            if (!IsCacheInitialized)
            {
                var fileService = MvxFileStoreHelper.SafeGetFileStore();
                fileService.EnsureFolderExists(_picCacheDir);

                // Remove old cached files
                var cacheFiles = fileService.GetFilesIn(_picCacheDir);
                if (cacheFiles != null)
                {
                    var expiredDay = DateTime.Now.AddDays(-CacheFileExpirationDays);
                    foreach (var file in cacheFiles)
                    {
                        var createdAt = System.IO.File.GetCreationTime(file);
                        if (createdAt < expiredDay)
                        {
                            fileService.DeleteFile(file);
                        }
                    }
                }

                if (_localFilesCache == null)
                {
                    _localFilesCache = new ConcurrentDictionary<string, Bitmap>();
                }

                if (_remoteFilesCache == null)
                {
                    _remoteFilesCache = new ConcurrentDictionary<string, CacheItem>();
                }

                IsCacheInitialized = true;
            }
        }

        ~MvxDynamicCompressedBitmapHelper()
        {
            Dispose(false);
        }

        private void FireImageChanged(Bitmap image)
        {
            var handler = ImageChanged;
            handler?.Invoke(this, new MvxValueEventArgs<Bitmap>(image));
        }

        private async Task RequestImageAsync(string imageSource)
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
                _cancellationSource = null;
            }

            var cancelTokenSource = new CancellationTokenSource();
            var cancelToken = cancelTokenSource.Token;
            _cancellationSource = cancelTokenSource;

            if (string.IsNullOrEmpty(imageSource))
            {
                await ShowDefaultImage().ConfigureAwait(false);
                return;
            }

            if (imageSource.ToUpper().StartsWith("HTTP"))
            {
                var error = false;
                try
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    if (_remoteFilesCache.ContainsKey(imageSource))
                    {
                        NewImageAvailable(_remoteFilesCache[imageSource].Image);
                        return;
                    }
                    else
                    {
                        _currentImageState = ImageState.DefaultShown;
                        FireImageChanged(null);
                    }

                    var image = await GetBitmap(imageSource);
                    if (image == null)
                    {
                        await ShowErrorImage().ConfigureAwait(false);
                    }
                    else
                    {
                        _remoteFilesCache.TryAdd(imageSource, new CacheItem
                        {
                            AddedAt = DateTime.Now,
                            Image = image
                        });
                        CheckRemoteCache();

                        var resultingBitmap = image;
                        if (image.Width > MaxWidth || image.Height > MaxHeight)
                        {
                            resultingBitmap = await Task.Run(() =>
                            {
                                int outWidth;
                                int outHeight;
                                var inWidth = image.Width;
                                var inHeight = image.Height;

                                if (inWidth > inHeight)
                                {
                                    outWidth = MaxWidth;
                                    outHeight = inHeight*MaxWidth/inWidth;
                                }
                                else
                                {
                                    outHeight = MaxHeight;
                                    outWidth = inWidth*MaxHeight/inHeight;
                                }

                                return Bitmap.CreateScaledBitmap(image, outWidth,
                                    outHeight,
                                    false);
                            }, cancelToken).ConfigureAwait(false);
                        }

                        NewImageAvailable(resultingBitmap);
                    }
                }
                catch (Exception ex)
                {
                    Mvx.Trace("failed to download image {0} : {1}", imageSource, ex.ToLongString());
                    error = true;
                }

                if (error)
                    await HttpImageErrorSeenAsync().ConfigureAwait(false);
            }
            else
            {
                try
                {
                    var image = await ImageFromLocalFileAsync(imageSource).ConfigureAwait(false);

                    if (cancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    if (image == null)
                        await ShowErrorImage().ConfigureAwait(false);
                    else
                        NewImageAvailable(image);
                }
                catch (Exception ex)
                {
                    Mvx.Error(ex.Message);
                }
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

                // Get image from memory cache if exists
                if (_localFilesCache.ContainsKey(filePath))
                {
                    FireImageChanged(_localFilesCache[filePath]);
                }
                else
                {
                    try
                    {
                        var localImage = await ImageFromLocalFileAsync(filePath).ConfigureAwait(false);
                        if (localImage == null)
                            MvxTrace.Warning("Failed to load local image for filePath {0}", filePath);
                        
                        // Put newly loaded image to the memory cache
                        _localFilesCache.TryAdd(filePath, localImage);
                        FireImageChanged(localImage);
                    }
                    catch (Exception ex)
                    {
                        Mvx.Error(ex.Message);
                    }
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

        private async Task<Bitmap> GetBitmap(string imageSource)
        {
            Bitmap image = null;

            var fileName = imageSource.Substring(imageSource.LastIndexOf('/') + 1);
            if (!fileName.Contains("."))
                fileName += ".bmp";

            var fullPath = string.Format("{0}{1}", _picCacheDir, fileName);

            var fileService = MvxFileStoreHelper.SafeGetFileStore();
            if (fileService.Exists(fullPath))
            {
                byte[] storedFile;
                fileService.TryReadBinaryFile(fullPath, out storedFile);

                if (storedFile != null && storedFile.Length > 0)
                {
                    try
                    {
                        image = BitmapFactory.DecodeByteArray(storedFile, 0, storedFile.Length);
                    }
                    catch (Exception ex)
                    {
                        Mvx.Error(ex.Message);
                    }
                }
            }
            else
            {
                var restClient = Mvx.Resolve<RestClient>();
                var data = await restClient.DownloadContent(imageSource).ConfigureAwait(false);
                fileService.WriteFile(fullPath, data);
                image = BitmapFactory.DecodeByteArray(data, 0, data.Length);
            }

            return image;
        }

        private void CheckRemoteCache()
        {
            if (_remoteFilesCache.Count > MaxMemoryCacheSize)
            {
                var itemsToDelete = _remoteFilesCache.OrderBy(i => i.Value.AddedAt).Take(10);
                foreach (var item in itemsToDelete)
                {
                    CacheItem removed;
                    _remoteFilesCache.TryRemove(item.Key, out removed);
                }
            }
        }

        protected virtual void Dispose(bool isDisposing)
        {
        }

        private class CacheItem
        {
            public DateTime AddedAt { get; set; }

            public Bitmap Image { get; set; }
        }
    }
}