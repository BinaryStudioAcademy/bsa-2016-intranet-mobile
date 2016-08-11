using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Fragments
{
    public class MenuFragmentViewModel : BaseFragmentViewModel
    {
        public MvxCommand ShowNewsCommand { get; set; }
        public MvxCommand LogoutCommand { get; set; }

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
    }
}