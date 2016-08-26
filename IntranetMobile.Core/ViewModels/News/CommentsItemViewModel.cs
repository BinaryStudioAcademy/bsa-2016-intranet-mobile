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

        string _newsId;

        public CommentsItemViewModel(CommentDto comment, string newsId)
        {
            _newsId = newsId;

            _name = GetAuthor(comment.authorId).Result;
            _date = DateTimeExtensions.UnixTimestampToDateTime(comment.date).ToString("dd-MM-yyyy HH:mm");
            _body = comment.body;
            _countLikes = comment.likes.Count;
            _isLiked = comment.likes.Contains(ServiceBus.UserService.CurrentUser.UserId);
            _commentId = comment.commentId;

            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        private async Task<string> GetAuthor(string authorId)
        {
            var author = await ServiceBus.UserService.GetUserById(authorId);
            return author.FirstName + " " + author.LastName;
        }

        public ICommand ClickLikeCommand { get; private set; }

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