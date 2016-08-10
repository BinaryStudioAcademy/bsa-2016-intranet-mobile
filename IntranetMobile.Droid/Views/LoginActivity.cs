using Android.App;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Droid.Views.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/BSTheme")]
    public class LoginActivity : MvxFragmentCompatActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Login);

            // Create a new fragment and a transaction.
            var fragmentTx = SupportFragmentManager.BeginTransaction();
            var loginFragment = new LoginFragment();

            // The fragment will have the ID of Resource.Id.fragment_container.
            fragmentTx.Replace(Resource.Id.login_fragment_container, loginFragment);

            // Commit the transaction.
            fragmentTx.Commit();
        }
    }
}
