using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using IntranetMobile.Core.ViewModels.News;
using IntranetMobile.Droid.Views.Controls;
using IntranetMobile.Droid.Views.Util;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "   ", Theme = "@style/BSTheme")]
    public class NewsDetailsActivity : MvxAppCompatActivity<NewsDetailsViewModel>
    {
        private LikeActionsWrapper _likeActionsWrapper;
        private CommentActionsWrapper _commentActionsWrapper;
        private Toolbar _toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_news_details);

            _toolbar = FindViewById<Toolbar>(Resource.Id.mvx_toolbar);

            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var webWiew = FindViewById<NewsDetailsWebView>(Resource.Id.activity_news_details_webview);
            webWiew.Settings.SetLayoutAlgorithm(WebSettings.LayoutAlgorithm.SingleColumn);
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
            likeActionsBindingSet.Bind(_likeActionsWrapper).For("IsLiked").To(viewModel => viewModel.NewsViewModel.IsLiked);
            likeActionsBindingSet.Bind(_likeActionsWrapper).For("LikesCount").To(viewModel => viewModel.NewsViewModel.LikesCount);
            likeActionsBindingSet.Apply();


            _commentActionsWrapper = new CommentActionsWrapper(menu);
            var commentActionsBindingSet = this.CreateBindingSet<NewsDetailsActivity, NewsDetailsViewModel>();
            commentActionsBindingSet.Bind(_commentActionsWrapper).For("CommentsCount").To(viewModel => viewModel.NewsViewModel.CommentsCount);
            commentActionsBindingSet.Apply();

            return base.OnCreateOptionsMenu(menu);
        }
    }
}