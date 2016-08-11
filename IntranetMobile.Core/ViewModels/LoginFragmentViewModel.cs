using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginFragmentViewModel : BaseFragmentViewModel
    {
        public LoginFragmentViewModel()
        {
            ForgotPasswordCommand = new MvxCommand(ForgotPassword);
        }

        public MvxCommand ForgotPasswordCommand { get; }

        private void ForgotPassword()
        {
            ShowViewModel<ForgotPasswordFragmentViewModel>();
        }
    }
}