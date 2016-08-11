namespace IntranetMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public override void Start()
        {
            base.Start();
            // TODO: Fill dat with tons of fancy code :3
            ShowViewModel<LoginFragmentViewModel>();
            //ShowViewModel<LoadingFrarmentViewModel>();
        }
    }
}