using IntranetMobile.Core.ViewModels.Fragments;

namespace IntranetMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public override void Start()
        {
            base.Start();
            ShowViewModel<MenuFragmentViewModel>();
        }
    }
}