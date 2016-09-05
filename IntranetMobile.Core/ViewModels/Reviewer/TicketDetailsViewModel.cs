using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketDetailsViewModel : BaseViewModel
    {
        private TicketDto _ticket;
        private string _ticketId;
        private UserTicketDto _userTicket;

        public string AuthorName => $"{UserTicket?.first_name} {UserTicket?.last_name}";

        public string CategoryName => Ticket?.group?.title;

        public string TicketText => Ticket?.details;

        public string ReviewDate => Ticket?.date_review;

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public ObservableCollection<TicketOfferViewModel> Offers { get; } =
            new ObservableCollection<TicketOfferViewModel>();

        public string TicketId
        {
            get { return _ticketId; }
            set
            {
                _ticketId = value;

                Task.Run(async () => { Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(_ticketId); });
            }
        }

        public TicketDto Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;

                Title = _ticket.title;

                foreach (var tagDto in _ticket.tags)
                {
                    InvokeOnMainThread(() => Tags.Clear());
                    InvokeOnMainThread(() => Tags.Add(new TagViewModel {TagName = tagDto.title}));
                }

                foreach (var userTicketDto in _ticket.users)
                {
                    InvokeOnMainThread(() => Offers.Clear());
                    InvokeOnMainThread(() => Offers.Add(new TicketOfferViewModel(userTicketDto.binary_id)));
                }

                RaisePropertyChanged(() => AuthorName);
                RaisePropertyChanged(() => CategoryName);
                RaisePropertyChanged(() => TicketText);
                RaisePropertyChanged(() => ReviewDate);
            }
        }

        public UserTicketDto UserTicket => Ticket?.user;

        public void Init(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}