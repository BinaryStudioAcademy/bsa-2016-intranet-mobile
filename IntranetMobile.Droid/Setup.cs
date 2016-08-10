using Android.Content;
using IntranetMobile.Core;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using IntranetMobile.Droid.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;

namespace IntranetMobile.Droid
{
    internal class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var application = new Application();
            return application;
        }

        protected override void InitializeLastChance()
        {
            Mvx.RegisterSingleton<ILogger>(new AndroidLogger());
            Mvx.RegisterType<IDataBaseService, DataBaseService>();
            Mvx.RegisterSingleton(Mvx.Resolve<IDataBaseService>());
            Mvx.RegisterSingleton<IStorageService>(new StorageService(ApplicationContext.FilesDir.Path)
            {
                DataBaseService = Mvx.Resolve<IDataBaseService>()
            });
            base.InitializeLastChance();
        }
    }
}