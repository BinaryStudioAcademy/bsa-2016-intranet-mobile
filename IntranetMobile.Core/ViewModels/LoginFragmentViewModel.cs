using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginFragmentViewModel : BaseFragmentViewModel
    {
        public LoginFragmentViewModel()
        {
            ForgotPasswordCommand = new MvxCommand(ForgotPassword);
            LoginCommand = new MvxCommand(Login);
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public MvxCommand ForgotPasswordCommand { get; }

        public MvxCommand LoginCommand { get; }

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