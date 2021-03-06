﻿using System;
using Windows.UI.Xaml.Controls;
using Intranet.WindowsUWP.Services;
using IntranetMobile.Core;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using MvvmCross.WindowsUWP.Platform;
using MvvmCross.WindowsUWP.Views;

namespace Intranet.WindowsUWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame, string suspensionManagerSessionStateKey = null) : base(rootFrame, suspensionManagerSessionStateKey)
        {
        }

        public Setup(IMvxWindowsFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<ILogger>(new WindowsLogger());
            Mvx.RegisterSingleton<IAlertService>(new AlertService());
            Mvx.RegisterSingleton<ISettingsService>(new SettingsService());
            Mvx.RegisterType<IMvxSqliteConnectionFactory, WindowsSqliteConnectionFactory>();
            Mvx.RegisterSingleton<IDataBaseService>(new DataBaseService(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                Mvx.Resolve<IMvxSqliteConnectionFactory>(),
                Mvx.Resolve<ILogger>()));

            var app = new Application();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LoginViewModel>());

            return app;
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            return new MvxWindowsMultiRegionViewPresenter(rootFrame);
        }
    }
}
