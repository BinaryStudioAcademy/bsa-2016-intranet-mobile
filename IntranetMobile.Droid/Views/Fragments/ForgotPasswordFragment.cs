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
    [MvxFragment(typeof(LoginViewModel), Resource.Id.login_fragment_container, true)]
    [Register("intranetmobile.droid.views.fragments.ForgotPasswordFragment")]
    public class ForgotPasswordFragment : MvxFragment<ForgotPasswordFragmentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.ForgotPasswordFragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            var backToLoginTextView = View.FindViewById<TextView>(Resource.Id.forgot_password_back_to_login_textview);
            backToLoginTextView.Click += ((LoginActivity)Activity).BackToLoginTextViewOnClick;
        }
    }
}