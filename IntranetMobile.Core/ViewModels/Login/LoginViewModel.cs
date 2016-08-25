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

            var credentials = await ServiceBus.StorageService.GetFirstOrDefault<Credentials>();
            if (credentials != null)
            {
                ShowViewModel<LoginLoadingViewModel>();
                var result = await ServiceBus.AuthService.Login(credentials.Email, credentials.Password);
                if (result.success)
                {
                    ShowViewModel<MainViewModel>();
                }
                else
                {
                    await ServiceBus.StorageService.RemoveItem(credentials);
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