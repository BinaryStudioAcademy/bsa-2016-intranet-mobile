using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Login
{
	public class ForgotPasswordViewModel : BaseViewModel
    {
		private string email;

        public ForgotPasswordViewModel()
        {
            BackToLoginCommand = new MvxCommand(backToLogin);
            SendCommand = new MvxCommand(send, canExecuteSend);
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public MvxCommand BackToLoginCommand { get; private set; }

        public MvxCommand SendCommand { get; private set; }

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