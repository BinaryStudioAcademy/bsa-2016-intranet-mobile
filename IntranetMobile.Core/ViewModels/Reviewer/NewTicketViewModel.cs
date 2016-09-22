using System;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class NewTicketViewModel : BaseViewModel
    {
        private string _ticketTitle;
        private string _details;
        private string _tags;
        private ReviewerGroup _group;
        private DateTime _date;
        private DateTimeOffset _dateTimeOffset;
        private TimeSpan _timeSpan;

        public NewTicketViewModel()
        {
            Title = "Add Review";
            Date = DateTime.Now;
            CreateTicketCommand = new MvxCommand(CreateTicketCommandExecute);
        }

        public ICommand CreateTicketCommand { get; set; }

        public Action NavigateBack { get; set; }

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

        public ReviewerGroup Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                RaisePropertyChanged(() => Group);
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

        public async void CreateTicketCommandExecute()
        {
            if (string.IsNullOrWhiteSpace(TicketTitle))
            {
                ServiceBus.AlertService.ShowMessageBox("Add Review", "Please fill the title of your review");
                return;
            }
            if (string.IsNullOrWhiteSpace(Details))
            {
                ServiceBus.AlertService.ShowMessageBox("Add Review", "Please fill the description of your review");
                return;
            }
            if ((Date - DateTime.Now).TotalHours < 1)
            {
                ServiceBus.AlertService.ShowMessageBox("Add Review", "Please check the date of your review");
                return;
            }
            if (Group == ReviewerGroup.None)
            {
                ServiceBus.AlertService.ShowMessageBox("Add Review", "Please select a category");
                return;
            }

            try
            {
                var ticket = new Ticket
                {
                    TitleName = TicketTitle,
                    DateReview = Date,
                    ReviewText = Details,
                    GroupId = ((int) Group).ToString()
                };


                var s = Tags.Replace(" ", ",")
                            .Replace(",,", ",")
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in s)
                {
                    ticket.ListOfTagTitles.Add(t);
                }

                await ServiceBus.ReviewerService.CreateReviewTicketAsync(ticket);
                NavigateBack?.Invoke();
                //ShowViewModel<ReviewerViewModel>();
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }
        }
    }
}

