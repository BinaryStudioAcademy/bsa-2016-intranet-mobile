using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<CommentsItemViewModel> _comments; 

        private string _newComment;
        private string _newsId;

        public CommentsViewModel()
        {
            ClickSaveCommentCommand = new MvxCommand(SaveCommentExecute);
        }

        public void Init(Parameters arg)
        {
            _newsId = arg.NewsId;

            GetComments();
        }

        public async void GetComments()
        {
            var comments = await ServiceBus.NewsService.LoadListOfCommentsAsync(_newsId);
            GetListOfComments(comments, _newsId);

            RaisePropertyChanged(() => Comments);
        }

        public void GetListOfComments(CommentsResponseDto listOfComments, string newsId)
        {
            _comments = new ObservableCollection<CommentsItemViewModel>();

            if (listOfComments == null) return;

            foreach (var c in listOfComments.comments)
            {
                var comment = new CommentsItemViewModel(c, newsId);

                _comments.Add(comment);
            }

        }

        public ICommand ClickSaveCommentCommand { get; private set; }

        public string NewComment
        {
            get { return _newComment;}
            set
            {
                _newComment = value;
                RaisePropertyChanged(() => NewComment);
            }
        }

        public ObservableCollection<CommentsItemViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged(() => Comments);
            }
        }

        private void SaveCommentExecute()
        {
            var b = ServiceBus.NewsService.AddCommentAsync(ServiceBus.UserService.CurrentUser.UserId, NewComment, _newsId);

            GetComments();

            NewComment = "";
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}

