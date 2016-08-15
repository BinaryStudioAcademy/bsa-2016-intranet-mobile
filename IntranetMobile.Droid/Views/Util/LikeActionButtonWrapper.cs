using Android.Views;
using MvvmCross.Platform.Core;

namespace IntranetMobile.Droid.Views.Util
{
    public class LikeActionButtonWrapper
    {
        private readonly IMenu _optionsMenu;

        private bool _isLiked;

        public LikeActionButtonWrapper(IMenu optionsMenu)
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
    }
}