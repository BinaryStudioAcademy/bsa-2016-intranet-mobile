using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Fragments;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public override async void Start()
        {
            base.Start();

            var user = await ServiceBus.StorageService.GetFirstOrDefault<User>();
            if (user != null)
            {
                ShowViewModel<LoadingFragmentViewModel>();
                var result = await ServiceBus.AuthService.Login(user.Email, user.Password);
                if (result.success)
                {
                    ShowViewModel<MainViewModel>();
                }
                else
                {
                    await ServiceBus.StorageService.RemoveItem(user);
                    ShowViewModel<LoginFragmentViewModel>();
                }
            }
            else
            {
                ShowViewModel<LoginFragmentViewModel>();
            }
        }
    }
}