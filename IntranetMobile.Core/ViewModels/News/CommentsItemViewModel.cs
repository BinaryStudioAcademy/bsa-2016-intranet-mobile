using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsItemViewModel : BaseViewModel
    {
        private string _name;
        private long _date;
        private string _body;
        private int _countLikes;
        private string _likeImageViewUrl;
        private bool _isLiked;

        public CommentsItemViewModel(string name, long date, string body, int countLikes)
        {
            _name = name;
            _date = date;
            _body = body;
            _countLikes = countLikes;

            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        public ICommand ClickLikeCommand { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public long Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                RaisePropertyChanged(() => Body);
            }
        }

        public int CountLikes
        {
            get { return _countLikes; }
            set
            {
                _countLikes = value;
                RaisePropertyChanged(() => CountLikes);
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

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                LikeImageViewUrl = _isLiked ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
            }
        }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
            CountLikes = IsLiked ? _countLikes + 1 : _countLikes - 1;
        }
    }
}

