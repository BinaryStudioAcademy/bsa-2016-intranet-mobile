using IntranetMobile.Core.ViewModels.Fragments;

namespace IntranetMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public override void Start()
        {
            base.Start();
            ShowViewModel<LoginFragmentViewModel>();
        }
    }
}