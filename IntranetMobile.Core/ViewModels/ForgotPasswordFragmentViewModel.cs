using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class ForgotPasswordFragmentViewModel : BaseFragmentViewModel
    {
        public ForgotPasswordFragmentViewModel()
        {
            BackToLoginCommand = new MvxCommand(ForgotPassword);
            SendCommand = new MvxCommand(Send);
        }

        public string Email { get; set; }

        public MvxCommand BackToLoginCommand { get; }

        public MvxCommand SendCommand { get; }

        private void Send()
        {
        }

        private void ForgotPassword()
        {
            ShowViewModel<LoginFragmentViewModel>();
        }
    }
}