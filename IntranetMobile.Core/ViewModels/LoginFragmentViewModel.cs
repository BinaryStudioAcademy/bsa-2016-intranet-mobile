using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginFragmentViewModel : BaseFragmentViewModel
    {
        private string _email = "tester_a@example.com";
        private string _errorText;
        private bool _hasErrors;
        private string _password = "123456";

        public LoginFragmentViewModel()
        {
            ForgotPasswordCommand = new MvxCommand(ForgotPassword);
            LoginCommand = new MvxCommand(Login, CanExecuteLogin);
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

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private async void Login()
        {
            ShowViewModel<LoadingFragmentViewModel>();
            var auth = await ServiceBus.AuthService.Login(Email, Password);
            if (auth.success)
            {
                ShowViewModel<NewsViewModel>();
            }
            else
            {
                HasErrors = true;
                ErrorText = "Login failed";
                ShowViewModel<LoginFragmentViewModel>();
            }
        }

        private void ForgotPassword()
        {
            ShowViewModel<ForgotPasswordFragmentViewModel>();
        }
    }
}