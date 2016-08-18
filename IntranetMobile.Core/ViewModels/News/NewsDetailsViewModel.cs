using System.Collections.Generic;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private Models.News _dataModel;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
        }

        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

        public string Body => _dataModel.Body;

        public int LikesCount => _dataModel.Likes?.Count ?? 0;

        public int CommentsCount => _dataModel.Comments?.Count ?? 0;

        public bool IsLiked => _dataModel.Likes?.Contains(ServiceBus.UserService.CurrentUser.UserId) ?? false;

        public async void Init(Parameters arg)
        {
            _dataModel = await ServiceBus.NewsService.GetCompanyNewsById(arg.NewsId);

            Title = _dataModel.Title;
            RaisePropertyChanged(() => LikesCount);
            RaisePropertyChanged(() => CommentsCount);
        }

        private async void Like()
        {
            if (!IsLiked)
            {
                var result = await ServiceBus.NewsService.LikeNews(_dataModel.NewsId);
                if (result)
                {
                    if (_dataModel.Likes == null)
                    {
                        _dataModel.Likes = new List<string>();
                    }
                    _dataModel.Likes.Add(ServiceBus.UserService.CurrentUser.UserId);
                    RaisePropertyChanged(() => IsLiked);
                    RaisePropertyChanged(() => LikesCount);
                }
            }
            else
            {
                var result = await ServiceBus.NewsService.UnLikeNews(_dataModel.NewsId);
                if (result)
                {
                    _dataModel.Likes.Remove(ServiceBus.UserService.CurrentUser.UserId);
                    RaisePropertyChanged(() => IsLiked);
                    RaisePropertyChanged(() => LikesCount);
                }
            }
        }

        private void Comment()
        {
            ShowViewModel<CommentsViewModel>();
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}