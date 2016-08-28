using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Intranet.WindowsUWP.Views;
using Intranet.WindowsUWP.Views.News;
using IntranetMobile.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Intranet.WindowsUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage
    {
        private int _prevSelectedMenuItem;

        public MainPage()
        {
            this.InitializeComponent();
            _prevSelectedMenuItem = 1;

            DataContextChanged += (sender, args) =>
            {
                var vm = ViewModel as MainViewModel;
                if (vm == null)
                    return;

                vm.Menu.ShowNews();
            };
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void MainMenu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = ViewModel as MainViewModel;
            if (vm == null ||
                MenuItemList.SelectedIndex == _prevSelectedMenuItem ||
                MenuItemList.SelectedIndex < 0)
                return;

            MenuListFooter.SelectedIndex = -1;

            switch (MenuItemList.SelectedIndex)
            {
                case 1:
                    vm.Menu.ShowNews();
                    break;
                case 3:
                    vm.Menu.ShowUsers();
                    break;
            }

            _prevSelectedMenuItem = MenuItemList.SelectedIndex;
        }

        private void MainMenuFooter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelectedIndex = MenuListFooter.SelectedIndex + 5;
            var vm = ViewModel as MainViewModel;
            if (vm == null ||
                currentSelectedIndex == _prevSelectedMenuItem ||
                MenuListFooter.SelectedIndex < 0)
                return;

            MenuItemList.SelectedIndex = -1;
            switch (currentSelectedIndex)
            {
                case 5:
                    vm.Menu.ShowSettings();
                    _prevSelectedMenuItem = currentSelectedIndex;
                    break;
                case 6:
                    if (_prevSelectedMenuItem > 4)
                        MenuListFooter.SelectedIndex = 0;
                    else
                    {
                        MenuListFooter.SelectedIndex = -1;
                        MenuItemList.SelectedIndex = _prevSelectedMenuItem;
                    }
                    vm.Menu.Logout();
                    break;
            }
        }
    }
}
