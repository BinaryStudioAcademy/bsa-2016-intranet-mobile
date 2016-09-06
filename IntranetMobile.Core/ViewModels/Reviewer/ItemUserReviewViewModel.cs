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
            ServiceBus.ReviewerService.DeleteTicketAsync(Id);
            if (NotifyItemDeleted != null)
            {
                NotifyItemDeleted.Invoke(VmId);
            }
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(new {ticketId = Id});
        }

        public static ItemUserReviewViewModel GetItemReviewViewModelFromDto(Ticket dto)
        {
            return new ItemUserReviewViewModel
            {
                AuthorImage = Constants.BaseUrl + dto.AuthorImage,
                Author = dto.AuthorName,
                DateTime = dto.DateReview,
                ReviewerText = dto.ReviewText,
                TitleName = dto.TitleName,
                Id = dto.TicketId
            };
        }
    }
}