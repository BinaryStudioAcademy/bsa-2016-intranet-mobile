using Android.App;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/BSTheme", NoHistory = true)]
    public class LoginActivity : MvxCachingFragmentCompatActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Login);
        }

        public override void OnBeforeFragmentChanging(
            IMvxCachedFragmentInfo fragmentInfo,
            FragmentTransaction transaction)
        {
            transaction.SetCustomAnimations(
                Android.Resource.Animation.SlideInLeft,
                Android.Resource.Animation.SlideOutRight);

            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }
    }
}