using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsViewModel : BaseViewModel
    {
        private string _newComment;
        private string _newsId;

        public CommentsViewModel()
        {
            Comments = new ObservableCollection<CommentsItemViewModel>();
            ClickSaveCommentCommand = new MvxCommand(SaveCommentExecute);
            Title = "Comments";
        }

        public void Init(Parameters arg)
        {
            _newsId = arg.NewsId;

            GetComments();
        }

        public async void GetComments()
        {
            IsBusy = true;
            var data = await ServiceBus.NewsService.LoadListOfCommentsAsync(_newsId);
            if (data == null) return;

            Comments.Clear();
            foreach (var c in data.comments)
            {
                var comment = new CommentsItemViewModel(c, _newsId);
                Comments.Add(comment);
            }
            IsBusy = false;
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

        public ObservableCollection<CommentsItemViewModel> Comments { get; private set; }

        private void SaveCommentExecute()
        {
            var b = ServiceBus.NewsService.AddCommentAsync(ServiceBus.UserService.CurrentUser.UserId, NewComment, _newsId);
            NewComment = "";

            GetComments();
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}

