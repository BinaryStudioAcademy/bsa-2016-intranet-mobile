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
            ClickSendCommentCommand = new MvxCommand(SendCommentExecute);
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

        public ICommand ClickSendCommentCommand { get; private set; }

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

        private async void SendCommentExecute()
        {
            if (string.IsNullOrWhiteSpace(NewComment))
                return;
            
            await ServiceBus.NewsService.AddCommentAsync(ServiceBus.UserService.CurrentUser.ServerId, NewComment, _newsId);
            NewComment = "";

            GetComments();
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}

