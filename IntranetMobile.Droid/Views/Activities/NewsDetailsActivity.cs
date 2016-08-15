using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "Intranet Mobile", Theme = "@style/BSTheme")]
    public class NewsDetailsActivity : MvxAppCompatActivity<NewsDetailsViewModel>
    {
        private Toolbar _toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_news_details);

            _toolbar = FindViewById<Toolbar>(Resource.Id.mvx_toolbar);

            SetSupportActionBar(_toolbar);
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
                case Resource.Id.menu_news_details_like:
                {
                    ViewModel.LikeCommand.Execute();
                    break;
                }
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_news_details, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}