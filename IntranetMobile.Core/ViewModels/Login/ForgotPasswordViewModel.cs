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
            SendCommand = new MvxCommand(Send, CanExecuteSend);
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public MvxCommand BackToLoginCommand { get; private set; }

        public MvxCommand SendCommand { get; }

        private async void Send()
        {
            try
            {
                var result = await ServiceBus.AuthService.ResetPassword(Email);
                string message;
                if (result)
                {
                    BackToLogin();
                    message = "Please check your email for further instructions";
                }
                else
                {
                    message = "Password reset failed";
                }

                ServiceBus.AlertService.ShowMessageBox("Password reset", message);
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }

        }

        private bool CanExecuteSend()
        {
            return !string.IsNullOrEmpty(Email);
        }

        private void BackToLogin()
        {
            Email = "";
            ShowViewModel<UserCredentialsViewModel>();
        }
    }
}