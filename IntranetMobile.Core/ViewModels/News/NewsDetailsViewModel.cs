using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";

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

        public bool IsLiked => false;

        public async void Init(Parameters arg)
        {
            var news = await ServiceBus.NewsService.GetCompanyNewsById(arg.NewsId);
            _dataModel = news;

            Title = news.Title;
            RaisePropertyChanged(() => LikesCount);
            RaisePropertyChanged(() => CommentsCount);
        }

        private void Like()
        {
            ServiceBus.AlertService.ShowMessage(Tag, "Like clicked!");
        }

        private void Comment()
        {
            ShowViewModel<CommentsViewModel>();
            ServiceBus.AlertService.ShowMessage(Tag, "Comment clicked!");
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}