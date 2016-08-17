using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace IntranetMobile.Core.Services
{
    public class ServiceBus
    {
        private static IStorageService _storageService;

        private static IAuthService _authService;

        private static INewsService _newsService;

        private static IAlertService _alertService;

        private static MvxMessengerHub _messengerHub;

        public static IStorageService StorageService
            => _storageService ?? (_storageService = Mvx.Resolve<IStorageService>());

        public static IAuthService AuthService
            => _authService ?? (_authService = Mvx.Resolve<IAuthService>());

        public static INewsService NewsService
            => _newsService ?? (_newsService = Mvx.Resolve<INewsService>());

        public static IAlertService AlertService
            => _alertService ?? (_alertService = Mvx.Resolve<IAlertService>());

        public static MvxMessengerHub MessengerHub
            => _messengerHub ?? (_messengerHub = new MvxMessengerHub());
    }
}