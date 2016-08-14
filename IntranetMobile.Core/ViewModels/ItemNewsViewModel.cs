using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class ItemNewsViewModel : BaseViewModel
    {
		private bool   isLiked;
        private string coverImageViewUrl;
        private string imageUri;
        private string likeImageViewUrl;

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
            get { return imageUri; }
            set
            {
                imageUri = value;
                RaisePropertyChanged(() => ImageUri);
            }
        }

        public ICommand ClickLikeCommand { get; private set; }
        public ICommand ClickCommentCommand { get; private set; }


        public bool IsLiked
        {
            get { return isLiked; }
            set
            {
                isLiked = value;
                LikeImageViewUrl = isLiked ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
            }
        }

        public string LikeImageViewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(likeImageViewUrl))
                {
                    likeImageViewUrl = "ic_favorite_border_white_24dp";
                }
                return likeImageViewUrl;
            }
            set
            {
                likeImageViewUrl = value;
                RaisePropertyChanged(() => LikeImageViewUrl);
            }
        }


        private void ClickLikeCommandExecute()
        {
            IsLiked = !isLiked;
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }
    }
}