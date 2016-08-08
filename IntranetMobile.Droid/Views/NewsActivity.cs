using Android.App;
using Android.Widget;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "NewsActivity", Theme = "@style/BSTheme")]
    public class NewsActivity : MvxAppCompatActivity<NewsViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.News);

            var toolbar = FindViewById<Toolbar>(Resource.Id.news_toolbar);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Binary studio";
            SupportActionBar.Subtitle = "News";
        }
    }
}