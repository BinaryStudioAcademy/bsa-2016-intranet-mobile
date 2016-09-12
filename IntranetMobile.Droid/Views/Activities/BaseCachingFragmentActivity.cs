using Android.Views;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Login;
using IntranetMobile.Droid.Receivers;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    public class BaseCachingFragmentActivity<TViewModel> : MvxCachingFragmentCompatActivity<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected override void OnResume()
        {
            base.OnResume();

            ViewModel?.Resume();
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (ServiceBus.UserService.CurrentUser != null
                && !(ViewModel is LoginViewModel))
            {
                var settings = ServiceBus.SettingsService.GetSettings();
                if (settings != null && (settings.IsNewsNotificationEnabled ||
                                         settings.IsReviewerNotificationEnabled))
                {
                    NotificationReceiver.Run(this, settings);
                }
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            NotificationReceiver.Stop();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            //Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
        }
    }
}