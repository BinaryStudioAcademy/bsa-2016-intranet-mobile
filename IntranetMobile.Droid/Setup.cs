using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using IntranetMobile.Core;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using IntranetMobile.Droid.Converters;
using IntranetMobile.Droid.Services;
using IntranetMobile.Droid.Views.Util;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Preference;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Sqlite;

namespace IntranetMobile.Droid
{
    internal class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly
        };

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
            Mvx.RegisterSingleton<IDeviceInfo>(new AndroidDeviceInfo());
            Mvx.RegisterSingleton<IAlertService>(new AlertService(Mvx.Resolve<IMvxAndroidCurrentTopActivity>()));
            Mvx.RegisterSingleton<ISettingsService>(new SettingsService(ApplicationContext));
            Mvx.RegisterSingleton<IDataBaseService>(new DataBaseService(
                ApplicationContext.FilesDir.Path,
                Mvx.Resolve<IMvxSqliteConnectionFactory>(),
                Mvx.Resolve<ILogger>()));
            Mvx.RegisterType<IMvxImageHelper<Bitmap>, MvxDynamicCompressedBitmapHelper>();
        }

        // Called after Application.Initialize()
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            MvxPreferenceSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("BoolToWhiteLikeIconConverter", new BoolToWhiteLikeIconConverter());
            registry.AddOrOverwrite("BoolToBlackLikeIconConverter", new BoolToBlackLikeIconConverter());
            registry.AddOrOverwrite("BoolToArrowIconConverter", new BoolToArrowIconConverter());
            registry.AddOrOverwrite("BoolToVisibilityConverter", new BoolToVisibilityConverter());
            registry.AddOrOverwrite("IntToInverseVisibilityConverter", new IntToInverseVisibilityConverter());
            registry.AddOrOverwrite("InvertedBoolToVisibilityConverter", new InvertedBoolToVisibilityConverter());
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}