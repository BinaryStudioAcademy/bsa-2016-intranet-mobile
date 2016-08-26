using Android.App;
using IntranetMobile.Core.ViewModels.News;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "CommentsActivity", Theme = "@style/BSTheme")]
    public class CommentsActivity : BaseToolbarActivity<CommentsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_comments;
    }
}