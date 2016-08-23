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
            set
            {
                _userId = value;
                Task.Run(async () => User = await ServiceBus.UserService.GetUserById(_userId));
            }
        }

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public DateTime Birthday => User?.Birthday ?? default(DateTime);

        public string Gender => User?.Gender;

        public string Country => User?.Country;

        public string City => User?.City;

        public DateTime HireDate => User?.HireDate ?? default(DateTime);

        public string Position => User?.Position;

        //public string AvatarUrl
        //    =>
        //        "https://material-design.storage.googleapis.com/publish/material_v_9/0Bx4BSt6jniD7RmdHSUxhbEFTR2s/style_imagery_introduction.png"
        //    ;

        public string AvatarUrl
            => User != null ? "http://team.binary-studio.com" + User.AvatarUri : null;

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}