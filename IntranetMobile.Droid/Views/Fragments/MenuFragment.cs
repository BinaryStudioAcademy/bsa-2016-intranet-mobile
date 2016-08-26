using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Droid.Views.Activities;
using IntranetMobile.Droid.Views.Controls;
using MvvmCross.Binding.Droid.BindingContext;

namespace IntranetMobile.Droid.Views.Fragments
{
    // We don't need to use this attribute
    // because of manual setting MenuFragment in MainActivity
    //[MvxFragment(typeof (MainViewModel), Resource.Id.menu_frame)]
    [Register("intranetmobile.droid.views.fragments.MenuFragment")]
    public class MenuFragment : BaseFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private MvxAppCompatCircleImageView _avatarImageView;
        private NavigationView _navigationView;
        private IMenuItem _selectedMenuItem;

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            _selectedMenuItem = menuItem;

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
            _selectedMenuItem = _navigationView.Menu.FindItem(Resource.Id.nav_news);
            _selectedMenuItem.SetChecked(true);

            _avatarImageView =
                _navigationView.GetHeaderView(0)
                    .FindViewById<MvxAppCompatCircleImageView>(Resource.Id.drawer_header_avatar_imageview);
            _avatarImageView.Click += (sender, args) =>
            {
                //_selectedMenuItem?.SetChecked(false);
                //((MainActivity) Activity).DrawerLayout.CloseDrawers();
                //await Task.Delay(250);
                ViewModel.ShowProfile();
            };

            return view;
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.ShowNews();
        }

        private async void Navigate(int itemId)
        {
            ((MainActivity) Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(250);

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
                    ViewModel.ShowSettings();
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