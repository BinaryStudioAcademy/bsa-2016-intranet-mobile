using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using IntranetMobile.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;

namespace Intranet.WindowsUWP.Views
{
    public class BasePage : MvxWindowsPage
    {
        public BasePage()
        {
            TransitionCollection collection = new TransitionCollection();
            NavigationThemeTransition theme = new NavigationThemeTransition();

            var info = new ContinuumNavigationTransitionInfo();

            theme.DefaultNavigationTransitionInfo = info;
            collection.Add(theme);
            this.Transitions = collection;
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (DataContext != null)
            {
                var vm = DataContext as BaseViewModel;
                vm?.Resume();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (DataContext != null)
            {
                var vm = DataContext as BaseViewModel;
                vm?.Pause();
            }
        }
    }
}
