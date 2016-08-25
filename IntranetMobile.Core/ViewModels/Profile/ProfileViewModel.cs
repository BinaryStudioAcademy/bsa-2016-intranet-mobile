using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
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
                });
            }
        }

        public string UserId
        {
            get { return _userId; }
            private set
            {
                _userId = value;
                Task.Run(async () => User = await ServiceBus.UserService.GetUserById(UserId));
            }
        }

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public DateTime Birthday => User?.Birthday ?? default(DateTime);

        public string Gender => User?.Gender;

        public string Country => User?.Country;

        public string City => User?.City;

        public DateTime HireDate => User?.HireDate ?? default(DateTime);

        public string Position => User?.PositionId;

        public string AvatarUrl => User != null
            ? Constants.BaseUrl + User.AvatarUri
            : null;

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}