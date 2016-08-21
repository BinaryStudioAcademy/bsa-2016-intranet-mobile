using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Platform;

namespace IntranetMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Menu = new MenuViewModel();
        }

        public override void Start()
        {
            base.Start();
            Menu.Start();
        }

        public MenuViewModel Menu { get; private set; }
    }
}