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
        private string _avatarUrl;
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

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                RaisePropertyChanged(() => AvatarUrl);
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
                    var credentials = await ServiceBus.StorageService.GetFirstOrDefault<Credentials>();
                    await ServiceBus.StorageService.RemoveItem(credentials);
                    ShowViewModel<LoginViewModel>();
                });
        }

        public override async void Start()
        {
            base.Start();

            var user = await ServiceBus.UserService.GetCurrentUserAsync();
            UserName = user.Email;
            AvatarUrl = Constants.BaseUrl + user.AvatarUri;
        }
    }
}