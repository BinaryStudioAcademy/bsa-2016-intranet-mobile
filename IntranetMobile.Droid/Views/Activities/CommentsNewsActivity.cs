using Android.App;
using IntranetMobile.Core.ViewModels.News;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "CommentsActivity",
              Theme = "@style/BSTheme",
              WindowSoftInputMode = Android.Views.SoftInput.AdjustPan,
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class CommentsNewsActivity : BaseToolbarActivity<CommentsNewsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_commentsnews;
    }
}