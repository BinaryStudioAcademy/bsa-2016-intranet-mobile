using System;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Fragments;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(NewsViewModel), Resource.Id.menu_frame)]
    [Register("intranetmobile.droid.views.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuFragmentViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView navigationView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuFragment, null);

            navigationView = view.FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.FindItem(Resource.Id.nav_news).SetChecked(true);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            menuItem.SetCheckable(true);
            menuItem.SetChecked(true);
            return true;
        }
    }
}