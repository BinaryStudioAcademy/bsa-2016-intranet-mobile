using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class ItemNewsViewModel : BaseViewModel
    {
        private string _coverImageViewUrl;
        private string _imageUri;
        private bool _isLiked;
        private string _likeImageViewUrl;

        public ItemNewsViewModel()
        {
            ClickCommentCommand = new MvxCommand(ClickCommentCommandExecute);
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
            ImageUri =
                "https://gallery.mailchimp.com/d962b18774558cf34c062e6b3/images/5bd435e8-9528-4dff-be19-828845e44bab.jpg";
        }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string NewsUrl { get; set; }

        public string ImageUri
        {
            get { return _imageUri; }
            set
            {
                _imageUri = value;
                RaisePropertyChanged(() => ImageUri);
            }
        }

        public ICommand ClickLikeCommand { get; private set; }
        public ICommand ClickCommentCommand { get; private set; }

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                LikeImageViewUrl = _isLiked ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
            }
        }

        public string LikeImageViewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_likeImageViewUrl))
                {
                    _likeImageViewUrl = "ic_favorite_border_white_24dp";
                }
                return _likeImageViewUrl;
            }
            set
            {
                _likeImageViewUrl = value;
                RaisePropertyChanged(() => LikeImageViewUrl);
            }
        }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }
    }
}