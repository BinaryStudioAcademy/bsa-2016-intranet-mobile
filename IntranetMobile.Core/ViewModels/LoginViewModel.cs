using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
		public LoginViewModel()
        {
            LoginCommand = new MvxCommand(Login);

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