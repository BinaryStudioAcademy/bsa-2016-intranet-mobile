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
        private UserInfo _user;
        private string _userId;

        public TicketOfferViewModel(string userId, string ticketId, bool isOfferForMyTicket)
        {
            Init(userId, ticketId, isOfferForMyTicket);
        }

        public MvxCommand AcceptCommand => new MvxCommand(AcceptExecute);

        public MvxCommand DeclineCommand => new MvxCommand(DeclineExecute);

        public Action<int> NotifyOfferDeleted { get; set; }

        public int VmId { get; set; }

        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;

                Task.Run(async () => { User = await ServiceBus.UserService.GetUserInfoById(UserId); });
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

        public string TicketId { get; set; }

        private async void DeclineExecute()
        {
            var result = await ServiceBus.ReviewerService.DeclineUserReviewForTicketAsync(UserId, TicketId);

            if (result)
            {
                NotifyOfferDeleted?.Invoke(VmId);
            }
        }

        private async void AcceptExecute()
        {
            await ServiceBus.ReviewerService.AcceptUserReviewForTicketAsync(UserId, TicketId);
        }

        public void Init(string userId, string ticketId, bool isOfferForMyTicket)
        {
            UserId = userId;
            TicketId = ticketId;
            IsOfferForMyTicket = isOfferForMyTicket;
        }
    }
}