using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemReviewViewModel : BaseItemReviewViewModel
    {
        private string _buttonText;
        private bool _isSigned;
        private string _userId;


        public ItemReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
            ClickSignCommand = new MvxCommand(ClickSignCommandxecute);
        }

        public ICommand ClickSignCommand { get; set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                RaisePropertyChanged(() => ButtonText);
            }
        }

        public bool IsSigned
        {
            get { return _isSigned; }
            set
            {
                _isSigned = value;
                ButtonText = _isSigned ? "Undo" : "Join";
            }
        }

        private async void ClickSignCommandxecute()
        {
            var newSignedValue = !_isSigned;
            if (newSignedValue)
            {
                await ServiceBus.ReviewerService.JoinTicketAsync(_userId, Id);
                ServiceBus.AlertService.ShowPopupMessage($"You joined \"{Title}\" by {Author}");
                IsSigned = newSignedValue;
            }
            else
            {
                ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                $"You will be unsubscribed from review \"{Title}\"",
                "Yes", "No", async () =>
                {
                    await ServiceBus.ReviewerService.UndoJoinTicketAsync(Id);
                    IsSigned = newSignedValue;
                });
            }
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(new {ticketId = Id});
        }

        public static ItemReviewViewModel FromModel(Ticket model, string currentUserId)
        {
            return new ItemReviewViewModel
            {
                //AuthorImage = Constants.BaseUrl + dto.AuthorImage,
                //Author = dto.AuthorName,
                DateTime = dto.DateReview,
                ReviewerText = dto.ReviewText,
                TitleName = dto.TitleName,
                Id = dto.TicketId,
                _userId = currentUserId,
                IsSigned = false
                //IsSigned need responce from server to know is user signed or not
            };
        }
    }
}