using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(LoginViewModel), Resource.Id.login_fragment_container)]
    [Register("intranetmobile.droid.views.fragments.LoginFragment")]
    public class LoginFragment : MvxFragment<LoginFragmentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.LoginFragment, null);
        }

        private void ForgotPasswordTextViewOnClick()
        {
            // Create a new fragment and a transaction.
            var fragmentTx = FragmentManager.BeginTransaction();
            var forgotPasswordFragment = new ForgotPasswordFragment();
            fragmentTx.SetCustomAnimations(Android.Resource.Animation.SlideInLeft,
                Android.Resource.Animation.SlideOutRight);
            // The fragment will have the ID of Resource.Id.login_fragment_container.
            fragmentTx.Replace(Resource.Id.login_fragment_container, forgotPasswordFragment, "ForgotPasswordFragment");
            // Commit the transaction.
            fragmentTx.Commit();
        }
    }
}