using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Profile;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments.Profile
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.profile.UsersProfilesFragment")]
    public class UsersProfilesFragment : BaseDrawerFragment<UsersViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.mvx_toolbar;
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_users;
    }
}