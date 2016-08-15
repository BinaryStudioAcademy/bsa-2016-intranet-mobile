namespace IntranetMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public override void Start()
        {
            base.Start();
            ShowViewModel<MenuViewModel>();
        }
    }
}