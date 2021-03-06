using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Core;

namespace IntranetMobile.Droid.Views.Util
{
    public class LikeActionsWrapper
    {
        private readonly IMenu _optionsMenu;

        private bool _isLiked;
        private int _likesCount;

        public LikeActionsWrapper(IMenu optionsMenu)
        {
            _optionsMenu = optionsMenu;
        }

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;

                var dispatcher = MvxMainThreadDispatcher.Instance;
                dispatcher.RequestMainThreadAction(() => SetLikeActionButtonState(_isLiked));
            }
        }

        public int LikesCount
        {
            get { return _likesCount; }
            set
            {
                _likesCount = value;

                var dispatcher = MvxMainThreadDispatcher.Instance;
                dispatcher.RequestMainThreadAction(() => SetLikesText(_likesCount));
            }
        }

        public void SetLikeActionButtonState(bool isLiked)
        {
            if (_optionsMenu == null)
            {
                return;
            }
            var refreshItem = _optionsMenu.FindItem(Resource.Id.menu_news_details_like);
            refreshItem.SetIcon(isLiked
                ? Resource.Drawable.ic_favorite_white_24dp
                : Resource.Drawable.ic_favorite_border_white_24dp);
        }

        public void SetLikesText(int likesCount)
        {
            if (_optionsMenu == null)
            {
                return;
            }
            var refreshItem = _optionsMenu.FindItem(Resource.Id.menu_news_details_likes_text);
            refreshItem.ActionView.FindViewById<TextView>(Resource.Id.menu_news_details_textview).Text =
                likesCount.ToString();
        }
    }
}