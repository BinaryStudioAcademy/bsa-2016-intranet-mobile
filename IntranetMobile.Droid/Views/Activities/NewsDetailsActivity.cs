using Android.App;
using Android.Views;
using Android.Webkit;
using IntranetMobile.Core.ViewModels.News;
using IntranetMobile.Droid.Views.Controls;
using IntranetMobile.Droid.Views.Util;
using MvvmCross.Binding.BindingContext;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class NewsDetailsActivity : BaseToolbarActivity<NewsDetailsViewModel>
    {
        private CommentActionsWrapper _commentActionsWrapper;
        private LikeActionsWrapper _likeActionsWrapper;

        public override int ActivityLayout { get; } = Resource.Layout.activity_news_details;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var webWiew = FindViewById<NewsDetailsWebView>(Resource.Id.activity_news_details_webview);
            webWiew.Settings.SetLayoutAlgorithm(WebSettings.LayoutAlgorithm.SingleColumn);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_news_details_like:
                {
                    ViewModel.LikeCommand.Execute();
                    break;
                }
                case Resource.Id.menu_news_details_comment:
                {
                    ViewModel.CommentCommand.Execute();
                    break;
                }
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_news_details, menu);

            _likeActionsWrapper = new LikeActionsWrapper(menu);
            var likeActionsBindingSet = this.CreateBindingSet<NewsDetailsActivity, NewsDetailsViewModel>();
            likeActionsBindingSet.Bind(_likeActionsWrapper)
                .For(l => l.IsLiked)
                .To(viewModel => viewModel.IsLiked);
            likeActionsBindingSet.Bind(_likeActionsWrapper)
                .For(l => l.LikesCount)
                .To(viewModel => viewModel.LikesCount);
            likeActionsBindingSet.Apply();


            _commentActionsWrapper = new CommentActionsWrapper(menu);
            var commentActionsBindingSet = this.CreateBindingSet<NewsDetailsActivity, NewsDetailsViewModel>();
            commentActionsBindingSet.Bind(_commentActionsWrapper)
                .For(c => c.CommentsCount)
                .To(viewModel => viewModel.CommentsCount);
            commentActionsBindingSet.Apply();

            return base.OnCreateOptionsMenu(menu);
        }
    }
}