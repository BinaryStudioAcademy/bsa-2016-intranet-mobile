using IntranetMobile.Core.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

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

        public async override void Start()
        {
            base.Start();

            // TODO: Fill dat with tons of fancy code :3

            var rest = Mvx.Resolve<IRestClient>();
            //await rest.GetAsync<object>("auth/logout", new { id = 0, lol = "lol", kek = 35 });
            var resp = await rest.GetAsync("auth/logout");
        }
    }
}