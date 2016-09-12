using System;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemUserReviewViewModel : BaseItemReviewViewModel
    {
        public ItemUserReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
            ClickDeleteTicketCommand = new MvxCommand(ClickDeleteTicketCommandExecute);
        }

        public ICommand ClickDeleteTicketCommand { get; private set; }

        public Action<int> NotifyItemDeleted { get; set; }

        private void ClickDeleteTicketCommandExecute()
        {
            ServiceBus.ReviewerService.DeleteTicketAsync(TicketId);
            NotifyItemDeleted?.Invoke(VmId);
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(new TicketDetailsViewModel.Parameters { TicketId = TicketId });
        }

        public static ItemUserReviewViewModel FromModel(Ticket model)
        {
            return new ItemUserReviewViewModel
            {
                TicketId = model.TicketId
            };
        }
    }
}