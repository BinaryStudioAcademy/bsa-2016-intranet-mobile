using Android.App;
using MvvmCross.Droid.Views;

namespace IntranetMobile.Droid.Views
{
    [Activity(Label = "IntranetMobile.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            // TODO: Fill in
        }
    }
}

