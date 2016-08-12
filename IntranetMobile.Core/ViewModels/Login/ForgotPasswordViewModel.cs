using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Login
{
	public class ForgotPasswordViewModel : BaseViewModel
    {
        private string _email;

        public ForgotPasswordViewModel()
        {
            BackToLoginCommand = new MvxCommand(backToLogin);
            SendCommand = new MvxCommand(send, canExecuteSend);
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

        private async void send()
        {
			var result = await ServiceBus.AuthService.ResetPassword(Email);
			if (result)
			{
				backToLogin();
			}
        }

        private bool canExecuteSend()
        {
            return !string.IsNullOrEmpty(Email);
        }

        private void backToLogin()
        {
            ShowViewModel<UserCredentialsViewModel>();
        }
    }
}