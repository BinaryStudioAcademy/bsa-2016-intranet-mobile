using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class MyProfileViewModel : BaseViewModel
    {
        private User _user;
        private string _userId;

        public MyProfileViewModel()
        {
            Title = "My Profile";
        }

        public User User
        {
            get { return _user; }
            private set
            {
                _user = value;
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => Surname);
                RaisePropertyChanged(() => Birthday);
                RaisePropertyChanged(() => Gender);
                RaisePropertyChanged(() => Country);
                RaisePropertyChanged(() => HireDate);
                RaisePropertyChanged(() => Position);
            }
        }

        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                Task.Run(async () => User = await ServiceBus.UserService.GetUserById(_userId));
            }
        }

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public DateTime Birthday => User.Birthday;

        public string Gender => "WIP";

        public string Country => "WIP";

        public string City => "WIP";

        public DateTime HireDate => default(DateTime);

        public string Position => User.Position;
    }
}