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
        private UserInfo _author;
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

        public UserInfo Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RefreshSutitile();
                RaisePropertyChanged(() => Author);
            }
        }

        public string AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                Task.Run(async () => { Author = await ServiceBus.UserService.GetUserInfoById(_authorId); });
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RefreshSutitile();
                RaisePropertyChanged(() => Date);
            }
        }

        public int LikesCount => _dataModel.Likes?.Count ?? 0;

        public int CommentsCount => _dataModel.Comments?.Count ?? 0;

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
            get
            {
                Task.Run(async () =>
                {
                    var user = await ServiceBus.UserService.GetCurrentUserAsync();
                    IsLiked = _dataModel.Likes.Contains(user.UserId);
                });

                return _isLiked;
            }
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

        private async void ClickLikeCommandExecute()
        {
            if (!IsLiked)
            {
                var result = await ServiceBus.NewsService.LikeNewsAsync(_dataModel.NewsId);
                if (result)
                {
                    RaisePropertyChanged(() => IsLiked);
                    RaisePropertyChanged(() => LikesCount);
                }
            }
            else
            {
                var result = await ServiceBus.NewsService.UnLikeNewsAsync(_dataModel.NewsId);
                if (result)
                {
                    RaisePropertyChanged(() => IsLiked);
                    RaisePropertyChanged(() => LikesCount);
                }
            }
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }

        private void RefreshSutitile()
        {
            Subtitle = Author != null
                ? $"{Author.FullName} on {Date.ToString("dd-MM-yyyy HH:mm")}"
                : $"{Date.ToString("dd-MM-yyyy HH:mm")}";
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