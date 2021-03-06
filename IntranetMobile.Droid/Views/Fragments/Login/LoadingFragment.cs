using Android.OS;
using Android.Runtime;
using Android.Views;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments.Login
{
    [MvxFragment(typeof(LoginViewModel), Resource.Id.login_fragment_container)]
    [Register("intranetmobile.droid.views.fragments.login.LoadingFragment")]
    public class LoadingFragment : BaseFragment<LoginLoadingViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.fragment_loading, null);
        }
    }
}