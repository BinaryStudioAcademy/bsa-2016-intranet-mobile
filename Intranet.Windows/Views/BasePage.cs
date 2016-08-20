using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using IntranetMobile.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;

namespace Intranet.WindowsUWP.Views
{
    public class BasePage<TViewModel> : MvxWindowsPage<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (DataContext != null)
            {
                var vm = DataContext as BaseViewModel;
                vm?.Resume();
            }
        }
    }
}
