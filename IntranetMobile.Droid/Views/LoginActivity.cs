using System;
using Android.App;
using Android.Widget;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Droid.Views.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/BSTheme")]
    public class LoginActivity : MvxCachingFragmentCompatActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Login);
        }

        public void ForgotPasswordTextViewOnClick(object sender, EventArgs eventArgs)
        {

        }

        public void BackToLoginTextViewOnClick(object sender, EventArgs eventArgs)
        {
            // Create a new fragment and a transaction.
            var fragmentTx = SupportFragmentManager.BeginTransaction();
            var loginFragment = new LoginFragment();
            fragmentTx.SetCustomAnimations(Android.Resource.Animation.SlideInLeft,
                Android.Resource.Animation.SlideOutRight);
            // The fragment will have the ID of Resource.Id.login_fragment_container.
            fragmentTx.Replace(Resource.Id.login_fragment_container, loginFragment, "LoginFragment");
            // Commit the transaction.
            fragmentTx.Commit();
        }
    }
}