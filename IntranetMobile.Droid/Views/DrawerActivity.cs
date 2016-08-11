using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views
{
    public abstract class DrawerActivity<T> : MvxCachingFragmentCompatActivity<T> where T : class, IMvxViewModel
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

        public override void OnBackPressed()
        {
            if (drawerLayout != null && drawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                drawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}