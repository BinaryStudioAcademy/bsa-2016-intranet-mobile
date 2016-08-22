using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    public abstract class BaseToolbarActivity<TViewModel> : MvxCachingFragmentCompatActivity<TViewModel>
        where TViewModel : BaseViewModel
    {
        private Toolbar _toolbar;
        public abstract int ActivityLayout { get; }
        public virtual int ToolbarLayout { get; } = Resource.Id.mvx_toolbar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(ActivityLayout);

            _toolbar = FindViewById<Toolbar>(ToolbarLayout);

            SetSupportActionBar(_toolbar);

            if (SupportActionBar != null)
            {
                SupportActionBar.Title = ViewModel.Title;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel?.Resume();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                {
                    Finish();
                    break;
                }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}