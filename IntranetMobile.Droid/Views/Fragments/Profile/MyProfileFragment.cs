using Android.OS;
using Android.Runtime;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Profile;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments.Profile
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.profile.MyProfileFragment")]
    public class MyProfileFragment : BaseFragment<MyProfileViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_my_profile, null);

            return view;
        }
    }
}