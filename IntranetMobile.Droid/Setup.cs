using Android.Content;
using IntranetMobile.Core;
using IntranetMobile.Core.Services;
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

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterSingleton<StorageService>(new StorageService(ApplicationContext.PackageCodePath + "db.db"));
            // TODO: Register IoC types here
            base.InitializeFirstChance();
        }
    }
}