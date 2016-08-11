using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IntranetMobile.Core
{
	public class Application : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();
			Mvx.RegisterSingleton(new RestClient());
		}
	}
}