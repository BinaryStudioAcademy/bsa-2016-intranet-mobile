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
            Task.Run(async () =>
            {
                await ServiceBus.UserService.GetCurrentUserAsync();
                InvokeOnMainThread(() =>
                {
                    RaisePropertyChanged(() => IsLiked);
                    RaisePropertyChanged(() => LikesCount);
                    RaisePropertyChanged(() => CommentsCount);
                });
            });
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

        public bool IsLiked => _dataModel.Likes.Contains(ServiceBus.UserService.CurrentUser.ServerId);

        private async void ClickLikeCommandExecute()
        {
            Task<bool> task;
            if (!IsLiked)
            {
                task = ServiceBus.NewsService.LikeNewsAsync(_dataModel.NewsId);
            }
            else
            {
                task = ServiceBus.NewsService.UnLikeNewsAsync(_dataModel.NewsId);
            }

            var result = await task;
            if (result)
            {
                RaisePropertyChanged(() => IsLiked);
                RaisePropertyChanged(() => LikesCount);
            }
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }

        private void RefreshSutitile()
        {
            Subtitle = Author != null
                ? $"{Author.FullName} on {Date.ToDateTimeString()}"
                : $"{Date.ToDateTimeString()}";
        }

        public override void Resume()
        {
            base.Resume();
            RaisePropertyChanged(() => IsLiked);
            RaisePropertyChanged(() => CommentsCount);
            RaisePropertyChanged(() => LikesCount);
        }

        public static NewsItemViewModel FromModel(Models.News news)
        {
            return new NewsItemViewModel
            {
                PreviewImageUri = news.Body != null ? news.Body.GetFirstImageUri() : "",
                NewsId = news.NewsId,
                Title = news.Title,
                AuthorId = news.AuthorId,
                Date = news.Date,
                _dataModel = news
            };
        }
    }
}