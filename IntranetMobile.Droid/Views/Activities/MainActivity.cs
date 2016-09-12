using Android.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Droid.Views.Fragments;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "Intranet",
              Theme = "@style/BSTheme",
              LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class MainActivity : BaseCachingFragmentActivity<MainViewModel>, IDrawerActivity
    {
        public DrawerLayout DrawerLayout { get; set; }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_main);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var menuViewModelInstance = ViewModel.Menu;
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.menu_frame, new MenuFragment {ViewModel = menuViewModelInstance})
                .Commit();
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);

            var extra = intent.GetStringExtra("current_fragment");
            if (!string.IsNullOrWhiteSpace(extra))
            {
                var itemId = intent.GetStringExtra("item_id");
                if (extra.Contains("news"))
                    ViewModel.Menu.ShowNewsDetails(itemId);
                else if (extra.Contains("weekly"))
                    ViewModel.Menu.ShowWeeklyDetails(itemId);
                else if (extra.Contains("reviewer"))
                    ViewModel.Menu.ShowReviewerDetails(itemId);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}