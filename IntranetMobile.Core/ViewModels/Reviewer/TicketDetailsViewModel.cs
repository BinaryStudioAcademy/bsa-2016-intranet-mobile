using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketDetailsViewModel : BaseViewModel
    {
        private UserInfo _author;
        private Ticket _ticket;
        private string _ticketId;

        public string AuthorAvatarUrl => Author != null ? Constants.BaseUrl + Author.AvatarUri : null;

        public string AuthorName => Author?.FullName;

        public string AuthorDepartment => Author?.Department;

        public string CategoryName => Ticket?.CategoryName;

        public string TicketText => Ticket?.ReviewText;

        public string ReviewDate => Ticket?.DateReview.ToDateTimeString("No date assigned");

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public ObservableCollection<TicketOfferViewModel> Offers { get; } =
            new ObservableCollection<TicketOfferViewModel>();

        public MvxCommand SignCommand => new MvxCommand(SignCommandExecute);

        public string TicketId
        {
            get { return _ticketId; }
            set
            {
                _ticketId = value;

                RefreshTicket();
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

                Title = Ticket.TitleName;
                
                // TODO: Maybe it will be good to check for previous properties' values before raising to avoid any kind of flickering

                InvokeOnMainThread(() => Tags.Clear());
                foreach (var tagDto in Ticket.ListOfTagTitles)
                {
                    InvokeOnMainThread(() => Tags.Add(new TagViewModel {TagName = tagDto}));
                }

                RefillOffers(Ticket.ListOfUsers);

                RaisePropertyChanged(() => CategoryName);
                RaisePropertyChanged(() => TicketText);
                RaisePropertyChanged(() => ReviewDate);
                RaisePropertyChanged(() => IsMyTicket);
                RaisePropertyChanged(() => IsSigned);
                RaisePropertyChanged(() => SignText);
            }
        }

        public bool IsSigned
            =>
                Ticket?.ListOfUsers?.FirstOrDefault(
                    userTicket => userTicket.BinaryId.Equals(ServiceBus.UserService.CurrentUser.ServerId)) != null;

        public string SignText => IsSigned ? "Undo" : "Join";

        private void RefreshTicket()
        {
            Task.Run(async () =>
            {
                Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId);
                Author = await ServiceBus.UserService.GetUserInfoById(Ticket.UserServerId);
            });
        }

        private async void SignCommandExecute()
        {
            if (!IsSigned)
            {
                var result =
                    await
                        ServiceBus.ReviewerService.JoinTicketAsync(ServiceBus.UserService.CurrentUser.ServerId, TicketId);
                ServiceBus.AlertService.ShowPopupMessage($"You joined \"{Title}\" by {Author}");
                if (result)
                {
                    RefreshTicket();
                    RefreshOffers();
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
                            RefreshTicket();
                            RefreshOffers();
                        }
                    });
            }
        }

        private async void RefreshOffers()
        {
            var ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId);
            RefillOffers(ticket.ListOfUsers);
        }

        private void RefillOffers(IEnumerable<Ticket.UserTicket> userTickets)
        {
            InvokeOnMainThread(() => Offers.Clear());
            foreach (var userTicket in userTickets)
            {
                InvokeOnMainThread(
                    () =>
                        Offers.Add(new TicketOfferViewModel(userTicket.BinaryId, userTicket.Id, TicketId,
                            Ticket.UserServerId.Equals(ServiceBus.UserService.CurrentUser.ServerId))
                        {
                            NotifyOfferDeleted = OfferDeleted
                        }));
            }
        }

        private void OfferDeleted(int vmId)
        {
            var foundTicketOfferViewModel =
                Offers.FirstOrDefault(ticketOfferViewModel => ticketOfferViewModel.VmId == vmId);
            InvokeOnMainThread(() => Offers.Remove(foundTicketOfferViewModel));
        }

        public void Init(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}