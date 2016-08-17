using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    public abstract class BaseToolbarActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
        where TViewModel : BaseViewModel
    {
        private Toolbar _toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_news_details);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = ViewModel.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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