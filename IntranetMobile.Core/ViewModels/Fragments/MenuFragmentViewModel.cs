using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Fragments
{
    public class MenuFragmentViewModel : BaseFragmentViewModel
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
            ShowViewModel<NewsFragmentViewModel>();
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

            UserName = (await ServiceBus.StorageService.GetFirstOrDefault<User>()).Email;
        }
    }
}