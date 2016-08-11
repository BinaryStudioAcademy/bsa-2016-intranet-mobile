using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginFragmentViewModel : BaseFragmentViewModel
    {
        private string _email;
        private string _password;

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

        public MvxCommand ForgotPasswordCommand { get; }

        public MvxCommand LoginCommand { get; }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void Login()
        {
            ShowViewModel<NewsViewModel>();
        }

        private void ForgotPassword()
        {
            ShowViewModel<ForgotPasswordFragmentViewModel>();
        }
    }
}