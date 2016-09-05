using Android.OS;
using Android.Runtime;
using Android.Views;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments.Login
{
    [MvxFragment(typeof(LoginViewModel), Resource.Id.login_fragment_container)]
    [Register("intranetmobile.droid.views.fragments.login.LoginFragment")]
    public class LoginFragment : BaseFragment<UserCredentialsViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.fragment_login, null);
        }
    }
}