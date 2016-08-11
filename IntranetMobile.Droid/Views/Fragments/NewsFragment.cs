using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Fragments;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.NewsFragment")]
    public class NewsFragment : MvxFragment<NewsFragmentViewModel>
    {
        public MvxCachingFragmentCompatActivity ParentActivity => ((MvxCachingFragmentCompatActivity)Activity);

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_news, null);

            var toolbar = view.FindViewById<Toolbar>(Resource.Id.news_toolbar);

            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.Title = "Binary studio";
            ParentActivity.SupportActionBar.Subtitle = "Fancy subheader";
            ParentActivity.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            return view;
        }
    }
}