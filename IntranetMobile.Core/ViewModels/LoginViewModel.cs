using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
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

		public override async void Start()
		{
			base.Start();
			var a = Mvx.Resolve<IStorageService>();
			//a.DataBaseService = Mvx.Resolve<IDataBaseService>();
			var user = new User() {Email = "123",Password = "123"};
			await a.AddItem<User>(user);
			var b = await a.GetAllItems<User>();
			// TODO: Fill dat with tons of fancy code :3
		}
	}
}