using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform;

namespace IntranetMobile.Core.Services
{
    public class ServiceBus
    {
        private static IStorageService storageService;

        private static IAuthService authService;

        private static INewsService newsService;

        private static IAlertService alertService;

        public static IStorageService StorageService
        {
            get
            {
                if (storageService == null)
                {
                    storageService = Mvx.Resolve<IStorageService>();
                }

                return storageService;
            }
        }

        public static IAuthService AuthService
        {
            get
            {
                if (authService == null)
                {
                    authService = Mvx.Resolve<IAuthService>();
                }

                return authService;
            }
        }

        public static INewsService NewsService
        {
            get
            {
                if (newsService == null)
                {
					newsService = Mvx.Resolve<INewsService>();
                }

                return newsService;
            }
        }

        public static IAlertService AlertService
        {
            get
            {
                if (alertService == null)
                {
                    //alertService = Get instance from IoC
                }

                return alertService;
            }
        }
    }
}