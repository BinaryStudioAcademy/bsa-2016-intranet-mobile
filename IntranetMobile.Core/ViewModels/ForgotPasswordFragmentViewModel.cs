using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class ForgotPasswordFragmentViewModel : BaseFragmentViewModel
    {
        public ForgotPasswordFragmentViewModel()
        {
            BackToLoginCommand = new MvxCommand(ForgotPassword);
        }

        public MvxCommand BackToLoginCommand { get; }

        private void ForgotPassword()
        {
            ShowViewModel<LoginFragmentViewModel>();
        }
    }
}