using System.Windows.Input;
using IntranetMobile.Core.Models;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private string _previewImageUri;
        private bool _isLiked;
        private string _likeImageViewUrl;
        private string _title;
        private string _subtitle;
        private string _newsUrl;
        private string _newsId;

        public NewsViewModel()
        {
            ClickCommentCommand = new MvxCommand(ClickCommentCommandExecute);
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set
            {
                _subtitle = value;
                RaisePropertyChanged(() => Subtitle);
            }
        }

        public string NewsUrl
        {
            get { return _newsUrl; }
            set
            {
                _newsUrl = value;
                RaisePropertyChanged(() => NewsUrl);
            }
        }

        public string NewsId
        {
            get { return _newsId; }
            set
            {
                _newsId = value;

                // TODO: Set all the staff here

                RaisePropertyChanged(() => NewsId);
            }
        }

        public string PreviewImageUri
        {
            get { return _previewImageUri; }
            set
            {
                _previewImageUri = value;
                RaisePropertyChanged(() => PreviewImageUri);
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