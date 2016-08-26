using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IntranetMobile.Core
{
    public class Application : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.RegisterSingleton(new RestClient());
            Mvx.RegisterSingleton<IUserService>(new UserService(Mvx.Resolve<RestClient>()));
            Mvx.RegisterType<IStorageService, StorageService>();
            Mvx.RegisterType<IAuthService, AuthService>();
            Mvx.RegisterType<INewsService, NewsService>();
            Mvx.RegisterType<ISettingsService, SettingsService>();
        }
    }
}