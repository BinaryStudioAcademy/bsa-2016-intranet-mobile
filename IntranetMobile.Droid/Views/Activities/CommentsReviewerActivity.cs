using Android.App;
using IntranetMobile.Core;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "CommentsActivity",
              Theme = "@style/BSTheme",
              WindowSoftInputMode = Android.Views.SoftInput.AdjustPan,
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class CommentsReviewerActivity : BaseToolbarActivity<CommentsReviewerViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_commentsreviewer;
    }
}