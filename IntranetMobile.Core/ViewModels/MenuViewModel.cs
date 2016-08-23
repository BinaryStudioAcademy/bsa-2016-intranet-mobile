using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Login;
using IntranetMobile.Core.ViewModels.News;
using IntranetMobile.Core.ViewModels.Profile;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userName;

        public MenuViewModel()
        {
            ShowProfileCommand = new MvxCommand(ShowProfile);
        }

        public MvxCommand ShowProfileCommand { get; }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public void ShowNews()
        {
            ShowViewModel<AllNewsViewModel>();
        }

        public async void ShowProfile()
        {
            var user = await ServiceBus.UserService.GetCurrentUserAsync();
            ShowViewModel<ProfileViewModel>(new {userId = user.UserId});
        }

        public void ShowUsers()
        {
            ShowViewModel<UsersViewModel>();
        }

        public async void Logout()
        {
            ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                "You will be logged out and your stored credentials will be removed",
                "Yes", "No", async () =>
                {
                    var logoutResult = await ServiceBus.AuthService.Logout();
                    if (!logoutResult)
                    {
                        // return;
                        // TODO: Log dat?
                    }
                    var user = await ServiceBus.StorageService.GetFirstOrDefault<User>();
                    await ServiceBus.StorageService.RemoveItem(user);
                    ShowViewModel<LoginViewModel>();
                });
        }

        public override async void Start()
        {
            base.Start();

            var currentUser = await ServiceBus.StorageService.GetFirstOrDefault<User>();
            if (currentUser != null)
            {
                UserName = currentUser.Email;
            }
        }
    }
}