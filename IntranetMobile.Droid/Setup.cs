using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using IntranetMobile.Core;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using IntranetMobile.Droid.Services;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Plugins;
using MvvmCross.Plugins.Sqlite;

namespace IntranetMobile.Droid
{
    internal class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                return new List<Assembly>(base.AndroidViewAssemblies)
                {
                    typeof (NavigationView).Assembly,
                    typeof (FloatingActionButton).Assembly,
                    typeof (Toolbar).Assembly,
                    typeof (DrawerLayout).Assembly,
                    typeof (ViewPager).Assembly
                };
            }
        }

        protected override IMvxApplication CreateApp()
        {
            var application = new Application();
            return application;
        }

        // Called before Application.Initialize()
        public override void Initialize()
        {


            base.Initialize();

            Mvx.RegisterSingleton<ILogger>(new AndroidLogger());
            Mvx.RegisterSingleton<IDataBaseService>(new DataBaseService(
                ApplicationContext.FilesDir.Path,
                Mvx.Resolve<IMvxSqliteConnectionFactory>(),
                Mvx.Resolve<ILogger>()));
        }

        // Called after Application.Initialize()
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

       

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}