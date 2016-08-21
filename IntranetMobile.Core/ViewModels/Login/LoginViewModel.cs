using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private const string Tag = "LoginViewModel";

        public LoginViewModel()
        {
            Title = "Login";
        }

        public override async void Resume()
        {
            base.Resume();

            var user = await ServiceBus.StorageService.GetFirstOrDefault<User>();
            if (user != null)
            {
                ShowViewModel<LoginLoadingViewModel>();
                var result = await ServiceBus.AuthService.Login(user.Email, user.Password);
                if (result.success)
                {
                    ShowViewModel<MainViewModel>();
                }
                else
                {
                    await ServiceBus.StorageService.RemoveItem(user);
                    ShowViewModel<UserCredentialsViewModel>();
                }
            }
            else
            {
                ShowViewModel<UserCredentialsViewModel>();
            }
        }
    }
}