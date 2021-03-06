using Android.App;
using Android.Views;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Droid.Shared.Caching;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "Intranet", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/BSTheme",
        NoHistory = true)]
    public class LoginActivity : BaseCachingFragmentActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.activity_login);
        }

        public override void OnBeforeFragmentChanging(
            IMvxCachedFragmentInfo fragmentInfo,
            FragmentTransaction transaction)
        {
            transaction.SetCustomAnimations(
                Resource.Animation.fastfadein,
                Resource.Animation.fastfadeout);

            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }
    }
}