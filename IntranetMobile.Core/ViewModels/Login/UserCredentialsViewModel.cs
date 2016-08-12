using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Login
{
	public class UserCredentialsViewModel : BaseViewModel
    {
        private string _email = "tester_a@example.com";
        private string _errorText;
        private bool _hasErrors;
        private string _password = "123456";

        public UserCredentialsViewModel()
        {
			ForgotPasswordCommand = new MvxCommand(showForgotPasswordVM);
            LoginCommand = new MvxCommand(login, canExecuteLogin);
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public bool HasErrors
        {
            get { return _hasErrors; }
            set
            {
                _hasErrors = value;
                RaisePropertyChanged(() => HasErrors);
            }
        }

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                RaisePropertyChanged(() => ErrorText);
            }
        }

        public MvxCommand ForgotPasswordCommand { get; }

        public MvxCommand LoginCommand { get; }

        private bool canExecuteLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private async void login()
        {
            ShowViewModel<LoginLoadingViewModel>();
            var auth = await ServiceBus.AuthService.Login(Email, Password);
            if (auth.success)
            {
                await ServiceBus.StorageService.AddItem(new User {Email = Email, Password = Password});
                ShowViewModel<MainViewModel>();
            }
            else
            {
                HasErrors = true;
                ErrorText = "Login failed";
                Password = string.Empty;
                ShowViewModel<UserCredentialsViewModel>();
            }
        }

        private void showForgotPasswordVM()
        {
            ShowViewModel<ForgotPasswordViewModel>();
        }
    }
}