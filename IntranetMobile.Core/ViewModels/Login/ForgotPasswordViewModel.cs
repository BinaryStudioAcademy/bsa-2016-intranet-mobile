using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Login
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string _email;

        public ForgotPasswordViewModel()
        {
            BackToLoginCommand = new MvxCommand(BackToLogin);
            SendCommand = new MvxCommand(send, CanExecuteSend);
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

        public MvxCommand BackToLoginCommand { get; private set; }

        public MvxCommand SendCommand { get; }

        private async void send()
        {
            var result = await ServiceBus.AuthService.ResetPassword(Email);
            if (result)
            {
                BackToLogin();
            }
        }

        private bool CanExecuteSend()
        {
            return !string.IsNullOrEmpty(Email);
        }

        private void BackToLogin()
        {
            ShowViewModel<UserCredentialsViewModel>();
        }
    }
}