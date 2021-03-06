﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Intranet.WindowsUWP.Services;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Intranet.WindowsUWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private bool _isActive;
        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Window.Current.CoreWindow.VisibilityChanged += (sender, args) =>
            {
                try
                {
                    if (args.Visible)
                    {
                        _isActive = true;
                        _cancellationToken?.Cancel();
                    }
                    else
                    {
                        _isActive = false;
                        _cancellationToken = new CancellationTokenSource();
                        Task.Run(async () =>
                        {
                            NotificationService.Run(ServiceBus.SettingsService.GetSettings());
                            while (true)
                            {
                                if (_cancellationToken != null && _cancellationToken.IsCancellationRequested)
                                    break;

                                await Task.Delay(TimeSpan.FromSeconds(NotificationService.CheckIntervalSec));
                                if (!_isActive)
                                    NotificationService.CheckServerState();
                            }
                        }, _cancellationToken.Token);
                    }
                }
                catch (Exception ex)
                {
                    //TODO: log
                }
            };

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    //rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    
                    var setup = new Setup(rootFrame);
                    setup.Initialize();

                    var start = Mvx.Resolve<IMvxAppStart>();
                    start.Start();

                }

                // Set min window size
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(580, 480));

                // Ensure the current window is active
                Window.Current.Activate();
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    var lightGrayColor = Color.FromArgb(255, 249, 249, 249);

                    titleBar.ButtonBackgroundColor = Colors.White;
                    titleBar.ButtonForegroundColor = Colors.Black;
                    titleBar.BackgroundColor = Colors.White;
                    titleBar.ForegroundColor = Colors.Black;
                }
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        
        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
