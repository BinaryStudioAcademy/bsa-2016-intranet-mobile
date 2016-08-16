using Android.App;
using IntranetMobile.Core.ViewModels.Login;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "Intranet Mobile", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/BSTheme",
        NoHistory = true)]
    public class LoginActivity : MvxCachingFragmentCompatActivity<LoginViewModel>
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