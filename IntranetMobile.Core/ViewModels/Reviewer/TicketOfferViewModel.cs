using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketOfferViewModel : BaseViewModel
    {
        private bool _isOfferForMyTicket;
        private Ticket _ticket;
        private string _ticketId;
        private UserInfo _user;
        private string _userBinaryId;

        public TicketOfferViewModel(string userBinaryId, string userId, string ticketId, bool isOfferForMyTicket)
        {
            Init(userBinaryId, userId, ticketId, isOfferForMyTicket);
        }

        public MvxCommand AcceptCommand => new MvxCommand(AcceptExecute);

        public MvxCommand DeclineCommand => new MvxCommand(DeclineExecute);

        public Action<int> NotifyOfferDeleted { get; set; }

        public int VmId { get; set; }

        public string UserBinaryBinaryId
        {
            get { return _userBinaryId; }
            set
            {
                _userBinaryId = value;

                Task.Run(async () => { User = await ServiceBus.UserService.GetUserInfoById(UserBinaryBinaryId); });
            }
        }

        public UserInfo User
        {
            get { return _user; }
            set
            {
                _user = value;

                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => AvatarUrl);
                RaisePropertyChanged(() => Position);
            }
        }

        public string AvatarUrl => User != null
            ? Constants.BaseUrl + User.AvatarUri
            : null;

        public string Name => User?.FullName;

        public string Position => User?.Department;

        public bool IsOfferForMyTicket
        {
            get { return _isOfferForMyTicket; }
            set
            {
                _isOfferForMyTicket = value;

                RaisePropertyChanged(() => IsOfferForMyTicket);
            }
        }

        public bool IsAccepted
            =>
                Convert.ToBoolean(
                    Ticket?.ListOfUsers?.Find(userTicket => userTicket.BinaryId.Equals(UserBinaryBinaryId))?.IsAccepted)
            ;

        public string TicketId
        {
            get { return _ticketId; }
            set
            {
                _ticketId = value;

                RaisePropertyChanged(() => TicketId);

                Refresh();
            }
        }

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;

                RaisePropertyChanged(() => Ticket);
                RaisePropertyChanged(() => IsAccepted);
            }
        }

        public string UserId { get; set; }

        private void Refresh()
        {
            Task.Run(async () => { Ticket = await ServiceBus.ReviewerService.GetTicketDetailsAsync(TicketId); });
        }

        private async void DeclineExecute()
        {
            var result = await ServiceBus.ReviewerService.DeclineUserReviewForTicketAsync(UserId, TicketId);

            if (result)
            {
                NotifyOfferDeleted?.Invoke(VmId);
                Refresh();
            }
        }

        private async void AcceptExecute()
        {
            var result = await ServiceBus.ReviewerService.AcceptUserReviewForTicketAsync(UserId, TicketId);

            if (result)
            {
                Refresh();
            }
        }

        public void Init(string userBinaryId, string userId, string ticketId, bool isOfferForMyTicket)
        {
            UserBinaryBinaryId = userBinaryId;
            UserId = userId;
            TicketId = ticketId;
            IsOfferForMyTicket = isOfferForMyTicket;
        }
    }
}