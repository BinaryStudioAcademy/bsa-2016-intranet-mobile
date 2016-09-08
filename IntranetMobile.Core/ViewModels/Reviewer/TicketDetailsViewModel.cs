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
        private bool _isSigned;
        private Ticket _ticket;
        private string _ticketId;

        public string AuthorAvatarUrl => Author != null ? Constants.BaseUrl + Author.AvatarUri : null;

        public string AuthorName => Author?.FullName;

        public string AuthorDepartment => Author?.Department;

        public string CategoryName => Ticket?.CategoryName;

        public string TicketText => Ticket?.ReviewText;

        public string ReviewDate => Ticket?.DateReview.ToDateTimeString();

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

                Task.Run(async () =>
                {
                    Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId);
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

                InvokeOnMainThread(() => Tags.Clear());
                foreach (var tagDto in _ticket.ListOfTagTitles)
                {
                    InvokeOnMainThread(() => Tags.Add(new TagViewModel {TagName = tagDto}));
                }

                InvokeOnMainThread(() => Offers.Clear());
                foreach (var userId in _ticket.ListOfUserIds)
                {
                    if (userId.Equals(ServiceBus.UserService.CurrentUser.ServerId))
                    {
                        IsSigned = true;
                    }

                    InvokeOnMainThread(
                        () =>
                            Offers.Add(new TicketOfferViewModel(userId, TicketId,
                                Ticket.UserServerId.Equals(ServiceBus.UserService.CurrentUser.ServerId))
                            {
                                NotifyOfferDeleted = OfferDeleted
                            }));
                }

                RaisePropertyChanged(() => CategoryName);
                RaisePropertyChanged(() => TicketText);
                RaisePropertyChanged(() => ReviewDate);
                RaisePropertyChanged(() => IsMyTicket);
            }
        }

        public bool IsSigned
        {
            get { return _isSigned; }
            set
            {
                _isSigned = value;

                RaisePropertyChanged(() => IsSigned);
                RaisePropertyChanged(() => SignText);
            }
        }

        public string SignText => IsSigned ? "Undo" : "Join";

        private async void SignCommandExecute()
        {
            var newSignedValue = !_isSigned;
            if (newSignedValue)
            {
                await ServiceBus.ReviewerService.JoinTicketAsync(ServiceBus.UserService.CurrentUser.ServerId, TicketId);
                ServiceBus.AlertService.ShowPopupMessage($"You joined \"{Title}\" by {Author}");
                IsSigned = true;
                RefreshOffers();
            }
            else
            {
                ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                    $"You will be unsubscribed from review \"{Title}\"",
                    "Yes", "No", async () =>
                    {
                        await ServiceBus.ReviewerService.UndoJoinTicketAsync(TicketId);
                        IsSigned = false;
                        RefreshOffers();
                    });
            }
        }

        private async void RefreshOffers()
        {
            var ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId);

            InvokeOnMainThread(() => Offers.Clear());
            foreach (var userId in ticket.ListOfUserIds)
            {
                InvokeOnMainThread(
                    () =>
                        Offers.Add(new TicketOfferViewModel(userId, TicketId,
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