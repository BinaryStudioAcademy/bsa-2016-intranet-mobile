using System;
using System.Collections.Generic;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Reviewer;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class NewTicketViewModel : BaseViewModel
    {
        private string _ticketTitle;
        private string _details;
        private string _tags;
        private int _groupId;
        private DateTime _date;

        public NewTicketViewModel()
        {
            ClickCreateTicketCommand = new MvxCommand(CreateTicket);
        }

        public ICommand ClickCreateTicketCommand { get; set; }

        public string TicketTitle
        {
            get
            {
                return _ticketTitle;
            }
            set
            {
                _ticketTitle = value;
                RaisePropertyChanged(() => TicketTitle);
            }
        }

        public string Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
                RaisePropertyChanged(() => Details);
            }
        }

        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                RaisePropertyChanged(() => Tags);
            }
        }

        public int GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                _groupId = value;
                RaisePropertyChanged(() => GroupId);
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public void CreateTicket()
        {
            try
            {
                var ticket = new Ticket();

                ticket.TitleName = TicketTitle;
                ticket.DateReview = Date != null
                    ? Date
                    : default(DateTime);
                ticket.ReviewText = Details;
                ticket.GroupId = (GroupId+1).ToString();

                var s = Tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in s)
                {
                    ticket.ListOfTagTitles.Add(t);
                }

                ServiceBus.ReviewerService.CreateReviewTicketAsync(ticket);
                ShowViewModel<ReviewerViewModel>();
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }

        }
    }
}

