using System.Threading.Tasks;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class CommentsReviewerItemViewModel: BaseViewModel
    {
        private string _body;
        private string _date;
        private string _name;
        private string _commentId;
        private string _previewImageUri;

        private string _reviewerId;

        public CommentsReviewerItemViewModel(Comment comment, string reviewerId)
        {
            _reviewerId = reviewerId;

            Name = GetAuthor(comment.AuthorId).Result;
            Date = comment.Date;
            Body = comment.Body.RemoveHTMLTags();
            _commentId = comment.CommentId;                  
        }

        private async Task<string> GetAuthor(string authorId)
        {
            var author = await ServiceBus.UserService.GetUserInfoById(authorId);
            if (author == null)
                author = await ServiceBus.UserService.GetUserInfoById(authorId, false);

            if (author != null)
            {
                PreviewImageUri = Constants.BaseUrl + author.AvatarUri;
                return author.FullName;
            }

            return "";
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
    }
}

