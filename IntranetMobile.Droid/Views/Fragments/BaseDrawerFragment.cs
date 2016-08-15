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
        private MvxActionBarDrawerToggle _drawerToggle;
        private Toolbar _toolbar;

        public MvxCachingFragmentCompatActivity ParentActivity => (MvxCachingFragmentCompatActivity) Activity;

        public int FragmentLayout { get; protected set; }
        public string Title { get; protected set; }
        public string Subtitle { get; protected set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentLayout, null);

            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            ParentActivity.SetSupportActionBar(_toolbar);
            ParentActivity.SupportActionBar.Title = Title;
            ParentActivity.SupportActionBar.Subtitle = Subtitle;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var drawerActivity = (IDrawerActivity) ParentActivity;

            _drawerToggle = new MvxActionBarDrawerToggle(
                Activity, // host Activity
                drawerActivity.DrawerLayout, // DrawerLayout object
                _toolbar, // nav drawer icon to replace 'Up' caret
                Resource.String.drawer_open, // "open drawer" description
                Resource.String.drawer_close // "close drawer" description
                );

            drawerActivity.DrawerLayout.AddDrawerListener(_drawerToggle);

            return view;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            _drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _drawerToggle.SyncState();
        }
    }
}