using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _position = string.Empty;
        private User _user;
        private string _userId;

        public User User
        {
            get { return _user; }
            private set
            {
                _user = value;
                InvokeOnMainThread(() =>
                {
                    RaisePropertyChanged(() => Name);
                    RaisePropertyChanged(() => Surname);
                    RaisePropertyChanged(() => Birthday);
                    RaisePropertyChanged(() => Gender);
                    RaisePropertyChanged(() => Country);
                    RaisePropertyChanged(() => HireDate);
                    RaisePropertyChanged(() => Position);
                    RaisePropertyChanged(() => AvatarUrl);
                    RaisePropertyChanged(() => Position);
                });
            }
        }

        public string UserId
        {
            get { return _userId; }
            private set
            {
                _userId = value;
                Task.Run(async () =>
                {
                    User = await ServiceBus.UserService.GetUserById(UserId);
                    Position = (await ServiceBus.UserService.GetPositionById(User.PositionId))?.Name ?? "?";
                });
            }
        }

        public string FullName => User?.FirstName;

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public string Birthday => (User?.Birthday ?? default(DateTime)).ToString("dd MMMM yyyy");

        public string Gender => User?.Gender;

        public string Country => User?.Country;

        public string City => User?.City;

        public string HireDate => (User?.HireDate ?? default(DateTime)).ToString("dd MM yyyy");

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
            }
        }

        public string AvatarUrl => User != null
            ? Constants.BaseUrl + User.AvatarUri
            : null;

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}