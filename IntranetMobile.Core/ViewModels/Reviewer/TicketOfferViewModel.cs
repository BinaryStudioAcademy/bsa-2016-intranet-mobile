using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketOfferViewModel : BaseViewModel
    {
        private string _position = string.Empty;
        private User _user;
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
                GetUserData();
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Name => User?.FullName;

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged();
            }
        }

        private void GetUserData()
        {
            Task.Run(async () =>
            {
                User = await ServiceBus.UserService.GetUserByServerId(UserId);
                var userInfo = await ServiceBus.UserService.GetUserInfoById(User.UserId);
                Position = userInfo?.Department ?? "?";
            });
        }

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}