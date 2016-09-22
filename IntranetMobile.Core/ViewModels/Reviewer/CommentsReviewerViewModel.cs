using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;
using IntranetMobile.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class CommentsReviewerViewModel : BaseViewModel
    {
        private string _newComment;
        private string _ticketId;

        public CommentsReviewerViewModel()
        {
            Comments = new ObservableCollection<CommentsReviewerItemViewModel>();
            SendCommentCommand = new MvxCommand(SendCommentExecute);
            Title = "Comments";
        }

        public void Init(Parameters arg)
        {
            _ticketId = arg.TicketId;
            GetComments();
        }

        public async void GetComments()
        {
            IsBusy = true;
            try
            {
                var data = await ServiceBus.ReviewerService.GetListOfTicketCommentsAsync(_ticketId);
                if (data == null) return;

                Comments.Clear();
                foreach (var c in data)
                {
                    var comment = new CommentsReviewerItemViewModel(c, _ticketId);
                    Comments.Add(comment);
                }
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }

            IsBusy = false;
        }

        public ICommand SendCommentCommand { get; private set; }

        public string NewComment
        {
            get { return _newComment; }
            set
            {
                _newComment = value;
                RaisePropertyChanged(() => NewComment);
            }
        }

        public ObservableCollection<CommentsReviewerItemViewModel> Comments { get; private set; }

        private async void SendCommentExecute()
        {
            if (string.IsNullOrWhiteSpace(NewComment))
                return;
            try
            {
                await ServiceBus.ReviewerService.WtiteCommentAsync(_ticketId, NewComment);           
                NewComment = "";
                GetComments();
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }
        }

        public class Parameters
        {
            public string TicketId { get; set; }
        }
    }
}

