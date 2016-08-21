using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments
{
    public class BaseFragment<TViewModel> : MvxFragment<TViewModel>
        where TViewModel : BaseViewModel
    {
        public override void OnResume()
        {
            base.OnResume();

            ViewModel?.Resume();
        }
    }
}