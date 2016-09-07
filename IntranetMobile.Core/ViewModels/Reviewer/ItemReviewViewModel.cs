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


        public ItemReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
            ClickSignCommand = new MvxCommand(ClickSignCommandxecute);
        }

        // TODO: Use CURRENT USER from service
        private string CurrentUserId { get; set; }

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
                await ServiceBus.ReviewerService.JoinTicketAsync(CurrentUserId, TicketId);
                ServiceBus.AlertService.ShowPopupMessage($"You joined \"{Title}\" by {Author}");
                IsSigned = true;
            }
            else
            {
                ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                    $"You will be unsubscribed from review \"{Title}\"",
                    "Yes", "No", async () =>
                    {
                        await ServiceBus.ReviewerService.UndoJoinTicketAsync(TicketId);
                        IsSigned = false;
                    });
            }
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(new {ticketId = TicketId});
        }

        public static ItemReviewViewModel FromModel(Ticket model, string currentUserId)
        {
            return new ItemReviewViewModel
            {
                TicketId = model.TicketId,
                CurrentUserId = currentUserId,
                IsSigned = false
                //IsSigned need responce from server to know is user signed or not
            };
        }
    }
}