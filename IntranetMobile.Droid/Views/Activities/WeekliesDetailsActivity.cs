using Android.App;
using Android.Content;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class WeekliesDetailsActivity : BaseToolbarActivity<WeekliesDetailsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_weeklies_details;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var list = FindViewById<MvxListView>(Resource.Id.weeklies_recycler_view);
            list.Adapter = new WeekliesAdapter(this, (IMvxAndroidBindingContext) BindingContext);
        }
    }

    public class WeekliesAdapter : MvxAdapter
    {
        public WeekliesAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        {
        }
    }
}