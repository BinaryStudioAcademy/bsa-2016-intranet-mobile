using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Users;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments.Users
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.users.UsersFragment")]
    public class UsersFragment : BaseDrawerFragment<UsersViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.mvx_toolbar;
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_users;
    }
}