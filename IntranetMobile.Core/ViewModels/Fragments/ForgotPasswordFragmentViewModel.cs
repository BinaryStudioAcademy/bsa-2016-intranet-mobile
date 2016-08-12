using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Fragments
{
    public class ForgotPasswordFragmentViewModel : BaseFragmentViewModel
    {
        private string _email;

        public ForgotPasswordFragmentViewModel()
        {
            BackToLoginCommand = new MvxCommand(ForgotPassword);
            SendCommand = new MvxCommand(Send, CanExecuteSend);
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public MvxCommand BackToLoginCommand { get; }

        public MvxCommand SendCommand { get; }

        private void Send()
        {
        }

        private bool CanExecuteSend()
        {
            return !string.IsNullOrEmpty(Email);
        }

        private void ForgotPassword()
        {
            ShowViewModel<LoginFragmentViewModel>();
        }
    }
}