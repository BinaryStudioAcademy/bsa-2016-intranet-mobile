using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Core.Services
{
    public class ServiceBus
    {
        private static IAuthService authService;
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

        private static INewsService newsService;
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

        private static IAlertService alertService;
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
