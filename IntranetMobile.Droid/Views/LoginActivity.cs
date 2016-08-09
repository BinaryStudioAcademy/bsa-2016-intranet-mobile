using Android.App;
using Android.Support.V7.Widget;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/BSTheme")]
    public class LoginActivity : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Login);
        }
    }
}
