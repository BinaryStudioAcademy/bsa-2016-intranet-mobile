using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketDetailsViewModel : BaseViewModel
    {
        private Ticket _ticket;
        private string _ticketId;
        private UserInfo _author;
        private bool _isMyTicket;

        public string AuthorAvatarUrl => Author != null ? Constants.BaseUrl + Author.AvatarUri : null;

        public string AuthorName => Author?.FullName;

        public string AuthorDepartment => Author?.Department;

        public string CategoryName => Ticket?.CategoryName;

        public string TicketText => Ticket?.ReviewText;

        public string ReviewDate => Ticket?.DateReview.ToDateTimeString();

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public ObservableCollection<TicketOfferViewModel> Offers { get; } =
            new ObservableCollection<TicketOfferViewModel>();

        public string TicketId
        {
            get { return _ticketId; }
            set
            {
                _ticketId = value;

                Task.Run(async () =>
                {
                    Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(_ticketId);
                    Author = await ServiceBus.UserService.GetUserInfoById(Ticket.UserServerId);
                });
            }
        }

        public bool IsMyTicket => Ticket.UserServerId.Equals(ServiceBus.UserService.CurrentUser.ServerId);

        public UserInfo Author
        {
            get { return _author; }
            set
            {
                _author = value;

                RaisePropertyChanged(() => AuthorAvatarUrl);
                RaisePropertyChanged(() => AuthorName);
                RaisePropertyChanged(() => AuthorDepartment);
            }
        }

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;

                Title = _ticket.TitleName;

                foreach (var tagDto in _ticket.ListOfTagTitles)
                {
                    InvokeOnMainThread(() => Tags.Clear());
                    InvokeOnMainThread(() => Tags.Add(new TagViewModel {TagName = tagDto}));
                }

                foreach (var userId in _ticket.ListOfUserIds)
                {
                    InvokeOnMainThread(() => Offers.Clear());
                    InvokeOnMainThread(() => Offers.Add(new TicketOfferViewModel(userId, Ticket.UserServerId.Equals(ServiceBus.UserService.CurrentUser.ServerId))));
                }

                RaisePropertyChanged(() => CategoryName);
                RaisePropertyChanged(() => TicketText);
                RaisePropertyChanged(() => ReviewDate);
                RaisePropertyChanged(() => IsMyTicket);
            }
        }

        public void Init(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}