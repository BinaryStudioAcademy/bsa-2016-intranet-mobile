using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IntranetMobile.Core.ViewModels.Reviewer;
using MvvmCross.Platform;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.Reviewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("MainContent")]
    public sealed partial class ReviewerPage : BasePage
    {
        public static ReviewerPage Instance { get; private set; }

        public Frame ContentFrame => TicketDetailsContent;

        public ReviewerPage()
        {
            Instance = this;
            this.InitializeComponent();
        }

        public void RefreshContent()
        {
            var vm = (ReviewerViewModel)DataContext;
            if (vm != null)
            {
                vm.DotNet.ReloadCommand.Execute(null);
                vm.JavaScript.ReloadCommand.Execute(null);
                vm.Php.ReloadCommand.Execute(null);
            }
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabs.SelectedIndex == 0)
            {
                RefresDotNetBtn.Visibility = Visibility.Visible;
                RefreshJsBtn.Visibility = Visibility.Collapsed;
                RefreshPhpBtn.Visibility = Visibility.Collapsed;

            }
            else if (Tabs.SelectedIndex == 1)
            {
                RefresDotNetBtn.Visibility = Visibility.Collapsed;
                RefreshJsBtn.Visibility = Visibility.Visible;
                RefreshPhpBtn.Visibility = Visibility.Collapsed;

            }
            else if (Tabs.SelectedIndex == 2)
            {
                RefresDotNetBtn.Visibility = Visibility.Collapsed;
                RefreshJsBtn.Visibility = Visibility.Collapsed;
                RefreshPhpBtn.Visibility = Visibility.Visible;

            }
        }
    }
}
