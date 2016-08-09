using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views
{
    public abstract class DrawerActivity<T> : MvxAppCompatActivity<T> where T : class, IMvxViewModel
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        protected abstract int LayoutResId { get; }
        protected abstract string Subtitle { get; }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(LayoutResId);

            var toolbar = FindViewById<Toolbar>(Resource.Id.news_toolbar);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Binary studio";
            SupportActionBar.Subtitle = Subtitle;
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                //react to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}