using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(LoginViewModel), Resource.Layout.LoginFragment, true)]
    public class LoginFragment : MvxFragment<LoginFragmentViewModel>
    {
    }
}