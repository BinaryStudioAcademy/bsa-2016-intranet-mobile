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
using IntranetMobile.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Intranet.WindowsUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : MvxWindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void MainMenu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = ViewModel as MainViewModel;
            if (vm == null)
                return;

            switch (MenuItemList.SelectedIndex)
            {
                case 0: 
                    vm.Menu.ShowNews();
                    break;
                case 1:
                    vm.Menu.ShowUsers();
                    break;
                case 6:
                    vm.Menu.Logout();
                    break;
            }
        }
    }
}
