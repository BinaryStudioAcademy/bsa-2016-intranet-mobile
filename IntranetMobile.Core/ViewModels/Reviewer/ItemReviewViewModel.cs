using System.Linq;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemReviewViewModel : BaseItemReviewViewModel
    {
        public ItemReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
            ClickSignCommand = new MvxCommand(ClickSignCommandxecute);
        }

        // TODO: Use CURRENT USER from service
        private string CurrentUserId { get; set; }

        public ICommand ClickSignCommand { get; set; }

        public override Ticket Ticket
        {
            get { return base.Ticket; }
            set
            {
                base.Ticket = value;

                RaisePropertyChanged(() => IsSigned);
                RaisePropertyChanged(() => ButtonText);
            }
        }

        public string ButtonText => IsSigned ? "Undo" : "Join";

        public bool IsSigned
            => Ticket.ListOfUsers?.FirstOrDefault(userTicket => userTicket.BinaryId.Equals(CurrentUserId)) != null;

        private async void ClickSignCommandxecute()
        {
            if (!IsSigned)
            {
                var result = await ServiceBus.ReviewerService.JoinTicketAsync(CurrentUserId, TicketId);
                ServiceBus.AlertService.ShowPopupMessage($"You joined \"{Title}\" by {Author}");
                if (result)
                {
                    Refresh();
                }
            }
            else
            {
                ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                    $"You will be unsubscribed from review \"{Title}\"",
                    "Yes", "No", async () =>
                    {
                        var result = await ServiceBus.ReviewerService.UndoJoinTicketAsync(TicketId);

                        if (result)
                        {
                            Refresh();
                        }
                    });
            }
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(new TicketDetailsViewModel.Parameters { TicketId = TicketId});
        }

        public static ItemReviewViewModel FromModel(Ticket model, string currentUserId)
        {
            return new ItemReviewViewModel
            {
                TicketId = model.TicketId,
                CurrentUserId = currentUserId
            };
        }
    }
}