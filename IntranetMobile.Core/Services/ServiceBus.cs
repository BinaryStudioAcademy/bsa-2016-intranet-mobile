using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Core.Services
{
    public class ServiceBus
    {
        private static IAuthService authService;

        private static INewsService newsService;

        private static IAlertService alertService;

        public static IAuthService AuthService
        {
            get
            {
                if (authService == null)
                {
                    //authService = Get instance from IoC
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
                    //newsService = Get instance from IoC
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