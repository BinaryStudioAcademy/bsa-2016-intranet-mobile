using System;
using IntranetMobile.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Plugins.DownloadCache;

namespace IntranetMobile.Droid.Views.Util
{
    public class MvxAuthorizedFileDownloadRequest
    {
        public MvxAuthorizedFileDownloadRequest(string url, string downloadPath)
        {
            Url = url;
            DownloadPath = downloadPath;
        }

        public string DownloadPath { get; }
        public string Url { get; }

        public event EventHandler<MvxFileDownloadedEventArgs> DownloadComplete;

        public event EventHandler<MvxValueEventArgs<Exception>> DownloadFailed;

        public async void Start()
        {
            try
            {
                var restClient = Mvx.Resolve<RestClient>();

                var fileService = MvxFileStoreHelper.SafeGetFileStore();
                var tempFilePath = DownloadPath + ".tmp";

                fileService.WriteFile(tempFilePath, await restClient.DownloadContent(Url));

                fileService.TryMove(tempFilePath, DownloadPath, true);
                FireDownloadComplete();
            }
                //#if !NETFX_CORE
                //            catch (ThreadAbortException)
                //            {
                //                throw;
                //            }
                //#endif
            catch (Exception e)
            {
                FireDownloadFailed(e);
            }
        }

        private void FireDownloadFailed(Exception exception)
        {
            var handler = DownloadFailed;
            handler?.Invoke(this, new MvxValueEventArgs<Exception>(exception));
        }

        private void FireDownloadComplete()
        {
            var handler = DownloadComplete;
            handler?.Invoke(this, new MvxFileDownloadedEventArgs(Url, DownloadPath));
        }
    }
}