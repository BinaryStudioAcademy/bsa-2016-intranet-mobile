using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketOfferViewModel : BaseViewModel
    {
        private UserInfo _user;
        private string _userId;
        private bool _isOfferForMyTicket;

        public TicketOfferViewModel(string userId, bool isOfferForMyTicket)
        {
            Init(userId, isOfferForMyTicket);
        }

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

        public void Init(string userId, bool isOfferForMyTicket)
        {
            UserId = userId;
            IsOfferForMyTicket = isOfferForMyTicket;
        }
    }
}