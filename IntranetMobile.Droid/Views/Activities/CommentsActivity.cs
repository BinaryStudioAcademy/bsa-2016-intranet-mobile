using Android.App;
using IntranetMobile.Core;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "CommentsActivity", Theme = "@style/BSTheme")]
    public class CommentsActivity : MvxAppCompatActivity<CommentsViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_comments);
        }
    }
}