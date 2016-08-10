using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public MvxCommand ForgotPasswordCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new MvxCommand(Login);
            ForgotPasswordCommand = new MvxCommand(ForgotPassword);
        }

        private void ForgotPassword()
        {
            
        }

        public MvxCommand LoginCommand { get; }

        private void Login()
        {
            ShowViewModel<NewsViewModel>();
        }

        public override void Start()
        {
            base.Start();

            // TODO: Fill dat with tons of fancy code :3

            
        }
    }
}