using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Helpers;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private string _authorId;
        private string _body;
        private int _commentsCount;
        private long _date;
        private bool _isLiked;
        private string _likeImageViewUrl;
        private int _likesCount;
        private string _newsId;
        private string _newsSubtitle;
        private string _newsTitle;
        private string _previewImageUri;
        private string _type;

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

                Task.Factory.StartNew(async delegate
                {
                    //await Task.Delay(1000); //TODO: Remove, experimantal
                    await FullReloadAsync();
                });
            }
        }

        public string NewsTitle
        {
            get { return _newsTitle; }
            set
            {
                _newsTitle = value;
                RaisePropertyChanged(() => NewsTitle);
            }
        }

        public string NewsSubtitle
        {
            get { return _newsSubtitle; }
            set
            {
                _newsSubtitle = value;
                RaisePropertyChanged(() => NewsSubtitle);
            }
        }

        public string AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                RaisePropertyChanged(() => AuthorId);
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

        public long Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
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
            get { return _likesCount; }
            set
            {
                _likesCount = value;
                RaisePropertyChanged(() => LikesCount);
            }
        }

        public int CommentsCount
        {
            get { return _commentsCount; }
            set
            {
                _commentsCount = value;
                RaisePropertyChanged(() => CommentsCount);
            }
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

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
        }

        private void ClickCommentCommandExecute()
        {
            //TODO: Show Comments Window
        }

        public async Task FullReloadAsync()
        {
            var news = await ServiceBus.NewsService.GetNewsByIdAsync(_newsId);
            NewsTitle = news.title;
            AuthorId = news.authorId;
            Body = news.body;
            Date = news.date;
            Type = news.type;
            LikesCount = news.likes.Count;
            CommentsCount = news.comments.Count;
            var author =
                (await ServiceBus.UserService.GetAllUsers()).FirstOrDefault(user => user.ServerUserId == news.authorId);
            NewsSubtitle =
                $"{author.Name} {author.Surname} on {TimeConvertHelper.ConvertFromUnixTimestamp(news.date)}";
        }

        public async Task MetadataReloadAsync()
        {
            var news = await ServiceBus.NewsService.GetNewsByIdAsync(_newsId);
            NewsTitle = news.title;
            LikesCount = news.likes.Count;
            CommentsCount = news.comments.Count;
            var author =
                (await ServiceBus.UserService.GetAllUsers()).FirstOrDefault(user => user.ServerUserId == news.authorId);
            NewsSubtitle =
                $"{author.Name} {author.Surname} on {TimeConvertHelper.ConvertFromUnixTimestamp(news.date)}";
        }
    }
}