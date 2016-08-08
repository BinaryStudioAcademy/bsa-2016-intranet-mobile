using Android.Content;
using IntranetMobile.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;

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
            // TODO: Register IoC types here
            base.InitializeFirstChance();
        }
    }
}