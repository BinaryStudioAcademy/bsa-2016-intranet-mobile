using Android.OS;
using Android.Runtime;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Fragments;
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

            return this.BindingInflate(Resource.Layout.fragment_login, null);
        }
    }
}