using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Core;

namespace IntranetMobile.Droid.Views.Util
{
    public class CommentActionsWrapper
    {
        private readonly IMenu _optionsMenu;

        private int _commentsCount;

        public CommentActionsWrapper(IMenu optionsMenu)
        {
            _optionsMenu = optionsMenu;
        }

        public int CommentsCount
        {
            get { return _commentsCount; }
            set
            {
                _commentsCount = value;

                var dispatcher = MvxMainThreadDispatcher.Instance;
                dispatcher.RequestMainThreadAction(() => SetCommentsText(_commentsCount));
            }
        }

        public void SetCommentsText(int commentsCount)
        {
            if (_optionsMenu == null)
            {
                return;
            }
            var refreshItem = _optionsMenu.FindItem(Resource.Id.menu_news_details_comments_text);
            refreshItem.ActionView.FindViewById<TextView>(Resource.Id.menu_news_details_textview).Text =
                commentsCount.ToString();
        }
    }
}