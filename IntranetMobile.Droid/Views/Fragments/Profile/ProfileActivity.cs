using Android.App;
using IntranetMobile.Core.ViewModels.Profile;
using IntranetMobile.Droid.Views.Activities;
using MvvmCross.Binding.BindingContext;

namespace IntranetMobile.Droid.Views.Fragments.Profile
{
    [Activity(Theme = "@style/BSTheme")]
    public class ProfileActivity : BaseToolbarActivity<ProfileViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_profile;

        public override int ToolbarLayout { get; } = Resource.Id.activity_profile_toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            // Used for RecyclerView issue eliminating. Testing for now.
            //var technologiesRecyclerView =
            //    FindViewById<MvxRecyclerView>(Resource.Id.content_profile_technologies_recycler_view);
            //technologiesRecyclerView.NestedScrollingEnabled = false;

            //var nestedScrollView = FindViewById<NestedScrollView>(Resource.Id.activity_profile_nestedscrollview);
            //technologiesRecyclerView.ChildViewAdded += delegate
            //{
            //    nestedScrollView.ScrollTo(0, 0);
            //};

            var bindingSet = this.CreateBindingSet<ProfileActivity, ProfileViewModel>();
            bindingSet.Bind(SupportActionBar)
                .For(bar => bar.Title)
                .To(vm => vm.FullName);
            bindingSet.Apply();
        }
    }
}