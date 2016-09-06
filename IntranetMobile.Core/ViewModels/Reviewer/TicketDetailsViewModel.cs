using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketDetailsViewModel : BaseViewModel
    {
        private Ticket _ticket;
        private string _ticketId;

        public string AuthorName => Ticket?.Author;

        public string CategoryName => Ticket?.CategoryName;

        public string TicketText => Ticket?.ReviewText;

        public string ReviewDate => Ticket?.DateReview;

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

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;

                Title = _ticket.TitleName;

                foreach (var tagDto in _ticket.ListOfTagTitle)
                {
                    InvokeOnMainThread(() => Tags.Clear());
                    InvokeOnMainThread(() => Tags.Add(new TagViewModel {TagName = tagDto}));
                }

                foreach (var userTicketDto in _ticket.ListOfUsersId)
                {
                    InvokeOnMainThread(() => Offers.Clear());
                    InvokeOnMainThread(() => Offers.Add(new TicketOfferViewModel(userTicketDto)));
                }

                RaisePropertyChanged(() => AuthorName);
                RaisePropertyChanged(() => CategoryName);
                RaisePropertyChanged(() => TicketText);
                RaisePropertyChanged(() => ReviewDate);
            }
        }

        public void Init(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}