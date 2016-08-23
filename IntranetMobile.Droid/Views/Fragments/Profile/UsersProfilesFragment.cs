using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Profile;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments.Profile
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.profile.UsersProfilesFragment")]
    public class UsersProfilesFragment : BaseDrawerFragment<UsersViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.mvx_toolbar;
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_users;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var list = view.FindViewById<MvxListView>(Resource.Id.list_view_users);
            list.Adapter = new UserAdapter(Context, (IMvxAndroidBindingContext) BindingContext);
            return view;
        }
    }

    public class UserAdapter : MvxAdapter
    {
        public UserAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context, bindingContext)
        {
        }

        public override int ViewTypeCount
        {
            get { return 2; }
        }

        public override int GetItemViewType(int position)
        {
            //var item = GetRawItem(position);
            if (position == 0)
                return 0;
            return 1;
        }

        protected override View GetBindableView(View convertView, object source, int templateId)
        {
            templateId = ItemsSource.ElementAt(0) == source ? Resource.Layout.current_user : Resource.Layout.item_user;
            return base.GetBindableView(convertView, source, templateId);
        }
    }
}