using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private Models.News _dataModel;
        private bool _isLiked;

        private string _newsId;
        private bool _visibility;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
            ChangeVisibilityCommand = new MvxCommand(ChangeVisibilityCommandExecute);
        }

        public MvxCommand ChangeVisibilityCommand { get; private set; }
        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

        public string Body => _dataModel.Body;

        public int LikesCount => _dataModel.Likes.Count;

        public int CommentsCount => _dataModel.Comments.Count;

        public bool IsLiked
        {
            get
            {
                Task.Run(async () =>
                {
                    var user = await ServiceBus.UserService.GetCurrentUserAsync();
                    IsLiked = _dataModel.Likes.Contains(user.ServerId);
                });

                return _isLiked;
            }
            set
            {
                _isLiked = value;
                RaisePropertyChanged(() => IsLiked);
            }
        }

        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        private void ChangeVisibilityCommandExecute()
        {
            Visibility = !_visibility;
        }

        public UserInfo Author { get; set; }

        public async void Init(Parameters arg)
        {
            _newsId = arg.NewsId;

            try
            {
                _dataModel = await ServiceBus.NewsService.GetNewsByIdAsync(_newsId);

                Title = _dataModel.Title;
                Author = await ServiceBus.UserService.GetUserInfoById(_dataModel.AuthorId);
                Subtitle = $" {Author.FullName} {_dataModel.Date.ToString("dd-MM-yyyy HH:mm")}";
                RaisePropertyChanged(() => LikesCount);
                RaisePropertyChanged(() => CommentsCount);
                RaisePropertyChanged(() => IsLiked);
                RaisePropertyChanged(() => Body);
            }
            catch
            {
            }
        }

        private async void Like()
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

       
        private void Comment()
        {
            ShowViewModel<CommentsViewModel>(new CommentsViewModel.Parameters {NewsId = _newsId});
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}