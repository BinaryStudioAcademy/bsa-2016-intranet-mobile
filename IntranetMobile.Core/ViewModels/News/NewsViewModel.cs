using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private User _author;
        private string _authorId;
        private string _body;
        private DateTime _date;
        private bool _isLiked;
        private string _likeImageViewUrl;
        private string _newsId;
        private string _previewImageUri;
        private string _type;

        private Models.News _dataModel;

        public NewsViewModel()
        {
            ClickCommentCommand = new MvxCommand(ClickCommentCommandExecute);
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        public string NewsId
        {
            get { return _newsId; }
            set
            {
                _newsId = value;
            }
        }

        public string Subtitle
        {
            get { return $"{_author.FirstName} {_author.LastName}     {Date.ToString("dd-MM-yyyy HH:mm")}"; }
        }

        public string AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                GetAuthor();
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

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
                RaisePropertyChanged(() => Subtitle);
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        public int LikesCount
        {
            get { return _dataModel.Likes != null ? _dataModel.Likes.Count : 0; }
        }

        public int CommentsCount
        {
            get { return _dataModel.Comments != null ? _dataModel.Comments.Count : 0; }
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
                RaisePropertyChanged(() => IsLiked);
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

        public override void Start()
        {
            base.Start();

            RaisePropertyChanged(() => LikesCount);
            RaisePropertyChanged(() => CommentsCount);
        }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }

        private void GetAuthor()
        {
            Task.Run(async () => 
            {
                _author = await ServiceBus.UserService.GetUserById(AuthorId);

                if (_author == null)
                    _author = new User();

                InvokeOnMainThread(() => RaisePropertyChanged(() => Subtitle));
            });
        }

        public static NewsViewModel FromModel(Models.News news)
        {
            return new NewsViewModel
            {
                Body = news.Body,
                PreviewImageUri = news.Body.GetFirstImageUri(),
                NewsId = news.NewsId,
                Title = news.Title,
                AuthorId = news.AuthorId,
                Date = news.Date,
                Type = news.Type,
                _dataModel = news
            };
        }
    }
}