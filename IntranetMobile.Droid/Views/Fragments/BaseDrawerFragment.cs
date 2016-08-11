using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Fragments
{
    public abstract class BaseDrawerFragment<TViewModel> : MvxFragment<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        private MvxActionBarDrawerToggle drawerToggle;
        private Toolbar toolbar;

        public MvxCachingFragmentCompatActivity ParentActivity => (MvxCachingFragmentCompatActivity) Activity;

        public abstract int FragmentLayout { get; }
        public abstract string Title { get; }
        public abstract string Subtitle { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentLayout, null);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.Title = Title;
            ParentActivity.SupportActionBar.Subtitle = Subtitle;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var drawerActivity = (IDrawerActivity) ParentActivity;

            drawerToggle = new MvxActionBarDrawerToggle(
                Activity, // host Activity
                drawerActivity.DrawerLayout, // DrawerLayout object
                toolbar, // nav drawer icon to replace 'Up' caret
                Resource.String.drawer_open, // "open drawer" description
                Resource.String.drawer_close // "close drawer" description
                );

            drawerActivity.DrawerLayout.AddDrawerListener(drawerToggle);

            return view;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            drawerToggle.SyncState();
        }
    }
}