using System;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsItemViewModel : BaseViewModel
    {
        private User _author;
        private string _authorId;

        private Models.News _dataModel;
        private DateTime _date;
        private bool _isLiked;
        private string _previewImageUri;

        public NewsItemViewModel()
        {
            ClickCommentCommand = new MvxCommand(ClickCommentCommandExecute);
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        public string NewsId { get; set; }

        public string NewsSubtitle => $"{_author.FirstName} {_author.LastName} {Date.ToString("dd-MM-yyyy HH:mm")}";

        public string AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                GetAuthor();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
                RaisePropertyChanged(() => NewsSubtitle);
            }
        }

        public int LikesCount
        {
            get { return _dataModel.Likes?.Count ?? 0; }
        }

        public int CommentsCount
        {
            get { return _dataModel.Comments?.Count ?? 0; }
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
                RaisePropertyChanged(() => IsLiked);
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
                _author = await ServiceBus.UserService.GetUserById(AuthorId) ?? new User();

                InvokeOnMainThread(() => RaisePropertyChanged(() => NewsSubtitle));
            });
        }

        public static NewsItemViewModel FromModel(Models.News news)
        {
            return new NewsItemViewModel
            {
                PreviewImageUri = news.Body.GetFirstImageUri(),
                NewsId = news.NewsId,
                Title = news.Title,
                AuthorId = news.AuthorId,
                Date = news.Date,
                _dataModel = news
            };
        }
    }
}