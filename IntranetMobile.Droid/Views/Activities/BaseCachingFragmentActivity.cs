using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    public class BaseCachingFragmentActivity<TViewModel> : MvxCachingFragmentCompatActivity<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected override void OnResume()
        {
            base.OnResume();

            ViewModel?.Resume();
        }
    }
}