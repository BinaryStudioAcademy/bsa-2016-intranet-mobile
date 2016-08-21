using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Droid.Views.Activities;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.menu_frame)]
    [Register("intranetmobile.droid.views.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            menuItem.SetCheckable(true);
            menuItem.SetChecked(true);

            Navigate(menuItem.ItemId);

            return true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_menu, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.nav_news).SetChecked(true);

            ViewModel.ShowNews();

            return view;
        }

        private async void Navigate(int itemId)
        {
            ((MainActivity) Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_news:
                {
                    ViewModel.ShowNews();
                    break;
                }
                case Resource.Id.nav_reviewer:
                {
                    ViewModel.ShowNews();
                    break;
                }
                case Resource.Id.nav_profile:
                {
                    ViewModel.ShowUsers();
                    break;
                }
                case Resource.Id.nav_asciit:
                {
                    ViewModel.ShowNews();
                    break;
                }
                case Resource.Id.nav_settings:
                {
                    ViewModel.ShowNews();
                    break;
                }
                case Resource.Id.nav_logout:
                {
                    ViewModel.Logout();
                    break;
                }
            }
        }
    }
}