using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Login;
using IntranetMobile.Core.ViewModels.News;
using IntranetMobile.Core.ViewModels.Users;

namespace IntranetMobile.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userName;

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

        public void ShowUsers()
        {
            ShowViewModel<UsersViewModel>();
        }

        public async void Logout()
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