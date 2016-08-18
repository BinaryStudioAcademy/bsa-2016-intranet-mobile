using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private const string Tag = "LoginViewModel";

        public override string Title { get; protected set; } = "Login";

        public override async void Start()
        {
            base.Start();

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
                    ServiceBus.AlertService.ShowMessage(Tag, result.message);
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