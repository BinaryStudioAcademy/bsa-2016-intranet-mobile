using Android.Content;
using IntranetMobile.Core;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using IntranetMobile.Droid.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;

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
    }
}