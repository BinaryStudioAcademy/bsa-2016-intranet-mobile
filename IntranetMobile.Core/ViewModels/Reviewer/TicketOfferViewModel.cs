using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketOfferViewModel : BaseViewModel
    {
        private UserInfo _user;
        private string _userId;

        public TicketOfferViewModel(string userId)
        {
            Init(userId);
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

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}