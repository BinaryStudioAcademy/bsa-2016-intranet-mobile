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
            ShowViewModel<MainViewModel>();
        }

        public async void Logout()
        {
            var logoutResult = await ServiceBus.AuthService.Logout();
            if (!logoutResult)
            {
                return;
            }
            await ServiceBus.StorageService.RemoveItem(await ServiceBus.StorageService.GetFirstOrDefault<User>());
            ShowViewModel<LoginViewModel>();
        }
    }
}