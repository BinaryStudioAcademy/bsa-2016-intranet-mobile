using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";

        private string _body;
        private Models.News _dataModel;

        public NewsDetailsViewModel()
        {
            Title = "";
            NewsViewModel = new NewsViewModel();

            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
        }

        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

        public NewsViewModel NewsViewModel { get; private set; }

        public async void Init(Parameters arg)
        {
            var news = await ServiceBus.NewsService.GetCompanyNewsById(arg.NewsId);
            _dataModel = news;

            Title = news.Title;
            Body = news.Body;

            RaisePropertyChanged(() => LikesCount);
            RaisePropertyChanged(() => CommentsCount);
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

        public int LikesCount
        {
            get { return _dataModel.Likes != null ? _dataModel.Likes.Count : 0; }
        }

        public int CommentsCount
        {
            get { return _dataModel.Comments != null ? _dataModel.Comments.Count : 0; }
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