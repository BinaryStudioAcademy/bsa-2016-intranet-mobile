using Android.OS;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(LoginViewModel), Resource.Id.login_fragment_container, true)]
    public class LoginFragment : MvxFragment<LoginFragmentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.LoginFragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            var forgotPasswordTextView = View.FindViewById<TextView>(Resource.Id.login_forgot_password_textview);
            forgotPasswordTextView.Click += ((LoginActivity)Activity).ForgotPasswordTextViewOnClick;
        }
    }
}