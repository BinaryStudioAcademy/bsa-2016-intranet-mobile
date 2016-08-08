using Android.App;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            // TODO: Fill dat with tons of fancy code :3
        }
    }
}