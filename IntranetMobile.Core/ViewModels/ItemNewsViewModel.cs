using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class ItemNewsViewModel : BaseViewModel
    {
        private bool _isLiked;
        private string coverImageViewUrl;
        private string likeImageViewUrl;
        private byte[] imageBytesArray;

        public ItemNewsViewModel()
        {
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        public ICommand ClickLikeCommand { get; private set; }
        public byte[] ImageBytesArray
        {
            get
            {
                return imageBytesArray;
            }
            set
            {
                imageBytesArray = value;
                RaisePropertyChanged(() => ImageBytesArray);
            }
        }
        public string CoverImageViewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(coverImageViewUrl))
                {
                    coverImageViewUrl = "logo_black_1";
                }
                return coverImageViewUrl;
            }
            set
            {
                coverImageViewUrl = value;
                RaisePropertyChanged(() => CoverImageViewUrl);
            }
        }

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

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string NewsUrl { get; set; }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
        }
    }
}