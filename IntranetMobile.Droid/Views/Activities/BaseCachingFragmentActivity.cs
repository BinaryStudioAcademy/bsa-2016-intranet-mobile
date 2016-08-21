using System;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid
{
    public class BaseCachingFragmentActivity<TViewModel> : MvxCachingFragmentCompatActivity<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (ViewModel != null)
                ViewModel.Resume();
        }
    }
}