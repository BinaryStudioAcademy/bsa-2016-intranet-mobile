using Android.App;
using IntranetMobile.Core.ViewModels;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "NewsActivity", Theme = "@style/BSTheme")]
    public class NewsActivity : DrawerActivity<NewsViewModel>
    {
        protected override int LayoutResId { get; } = Resource.Layout.News;
        protected override string Subtitle { get; } = "News";
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

           // SetContentView(Resource.Layout.News);
        }
    }
}