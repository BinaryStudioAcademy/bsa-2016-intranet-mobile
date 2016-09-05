﻿using System.Windows.Input;
using IntranetMobile.Core.Models.Dtos;
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


        private void ClickDeleteTicketCommandExecute()
        {
            ServiceBus.ReviewerService.DeleteTicketAsync(Id);
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>(Id);
        }

        public static ItemUserReviewViewModel GetItemReviewViewModelFromDto(TicketDto dto)
        {
            return new ItemUserReviewViewModel
            {
                AuthorImage = Constants.BaseUrl + dto.user.avatar,
                Author = $"{dto.user.first_name} {dto.user.last_name}",
                DateTime = dto.date_review,
                ReviewerText = dto.details,
                TitleName = dto.title,
                Id = dto.id
            };
        }
    }
}