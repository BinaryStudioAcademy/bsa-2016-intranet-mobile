using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class BaseItemReviewViewModel : BaseViewModel
    {
        private UserInfo _author;
        private Ticket _ticket;
        private string _ticketId;

        public ICommand ClickViewDetailsCommand { get; set; }

        public int VmId { get; set; }

        public string TicketId
        {
            get { return _ticketId; }
            set
            {
                _ticketId = value;

                Task.Run(async () =>
                {
                    Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId);
                    Title = Ticket.TitleName;
                    Author = await ServiceBus.UserService.GetUserInfoById(Ticket.UserServerId);
                });
            }
        }

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;

                RaisePropertyChanged(() => ReviewText);
                RaisePropertyChanged(() => DateTime);
            }
        }

        public UserInfo Author
        {
            get { return _author; }
            set
            {
                _author = value;

                RaisePropertyChanged(() => AuthorAvatarUrl);
                RaisePropertyChanged(() => AuthorName);
            }
        }

        public string AuthorAvatarUrl => Author != null ? Constants.BaseUrl + Author.AvatarUri : null;

        public string AuthorName => Author?.FullName;

        public string DateTime => Ticket?.DateReview.ToDateTimeString();

        public string ReviewText => Ticket?.ReviewText;
    }
}