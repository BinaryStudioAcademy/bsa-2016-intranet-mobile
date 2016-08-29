using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsItemViewModel : BaseViewModel
    {
        private string _body;
        private int _countLikes;
        private string _date;
        private bool _isLiked;
        private string _name;
        private string _commentId;
        private string _previewImageUri;

        private string _newsId;

        public CommentsItemViewModel(CommentDto comment, string newsId)
        {
            _newsId = newsId;

            Name = GetAuthor(comment.authorId).Result;
            Date = DateTimeExtensions.UnixTimestampToDateTime(comment.date).ToString("dd-MM-yyyy HH:mm");
            Body = comment.body.RemoveHTMLTags();
            CountLikes = comment.likes.Count;
            IsLiked = comment.likes.Contains(ServiceBus.UserService.CurrentUser.UserId);
            _commentId = comment.commentId;

            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        private async Task<string> GetAuthor(string authorId)
        {
            var author = await ServiceBus.UserService.GetUserInfoById(authorId);
            PreviewImageUri = Constants.BaseUrl + author.AvatarUri;
            return author.FullName;
        }

        public ICommand ClickLikeCommand { get; private set; }

        public string PreviewImageUri
        {
            get { return _previewImageUri; }
            set
            {
                _previewImageUri = value;
                RaisePropertyChanged(() => PreviewImageUri);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
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

        public int CountLikes
        {
            get { return _countLikes; }
            set
            {
                _countLikes = value;
                RaisePropertyChanged(() => CountLikes);
            }
        }

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                RaisePropertyChanged(() => IsLiked);
            }
        }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
            if (IsLiked)
            {
                CountLikes = _countLikes + 1;
                ServiceBus.NewsService.LikeCommentAsync(_newsId, _commentId);
            }
            else
            {
                CountLikes = _countLikes - 1;
                ServiceBus.NewsService.UnlikeCommentAsync(_newsId, _commentId);
            }
        }
    }
}