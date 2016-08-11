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