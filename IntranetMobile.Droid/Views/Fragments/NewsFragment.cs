using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Fragments;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.NewsFragment")]
    public class NewsFragment : MvxFragment<NewsFragmentViewModel>
    {
        private MvxActionBarDrawerToggle drawerToggle;
        private Toolbar toolbar;

        public MvxCachingFragmentCompatActivity ParentActivity => (MvxCachingFragmentCompatActivity) Activity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_news, null);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.Title = "Binary studio";
            ParentActivity.SupportActionBar.Subtitle = "Fancy subheader";
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