using Android.App;
using IntranetMobile.Core.ViewModels.News;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "Intranet Mobile", Theme = "@style/BSTheme")]
    public class NewsDetailsActivity : BaseToolbarActivity<NewsDetailsViewModel>
    {
        public override string ToolbarTitle { get; protected set; } = "News title";

        public override string ToolbarSubtitle { get; protected set; } = "News subtitile";
    }
}