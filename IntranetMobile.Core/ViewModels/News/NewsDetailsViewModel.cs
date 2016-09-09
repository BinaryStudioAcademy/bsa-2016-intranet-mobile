using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private Models.News _dataModel;
        private string _newsId;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
        }

        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

        public string Body => _dataModel.Body;

        public int LikesCount => _dataModel.Likes.Count;

        public int CommentsCount => _dataModel.Comments.Count;

        public bool IsLiked => _dataModel.Likes.Contains(ServiceBus.UserService.CurrentUser.ServerId);

        public UserInfo Author { get; set; }

        public async void Init(Parameters arg)
        {
            _newsId = arg.NewsId;

            try
            {
                _dataModel = await ServiceBus.NewsService.GetNewsByIdAsync(_newsId);

                Title = _dataModel.Title;
                Author = await ServiceBus.UserService.GetUserInfoById(_dataModel.AuthorId);
                Subtitle = _dataModel.Date.ToDateTimeString();
                RaisePropertyChanged(() => LikesCount);
                RaisePropertyChanged(() => CommentsCount);
                RaisePropertyChanged(() => IsLiked);
                RaisePropertyChanged(() => Body);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
        }

        private async void Like()
        {
            Task<bool> task;
            if (IsLiked)
                task = ServiceBus.NewsService.UnLikeNewsAsync(_dataModel.NewsId);
            else
                task = ServiceBus.NewsService.LikeNewsAsync(_dataModel.NewsId);

            var result = await task;
            if (result)
            {
                RaisePropertyChanged(() => IsLiked);
                RaisePropertyChanged(() => LikesCount);
            }
        }

        public override void Resume()
        {
            base.Resume();
            RaisePropertyChanged(() => CommentsCount);
        }
       
        private void Comment()
        {
            ShowViewModel<CommentsNewsViewModel>(new CommentsNewsViewModel.Parameters {NewsId = _newsId});
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}