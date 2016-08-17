using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IntranetMobile.Droid.Views.Fragments.News
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.news.NewsFragment")]
    public class NewsFragment : BaseDrawerFragment<AllNewsViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.tabbed_toolbar;
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_news;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxFragmentPagerAdapter.FragmentInfo>
                {
                    new MvxFragmentPagerAdapter.FragmentInfo("COMPANY", typeof (NewsRecyclerViewFragment),
                        typeof (CompanyViewModel)),
                    new MvxFragmentPagerAdapter.FragmentInfo("WEEKLIES", typeof (NewsRecyclerViewFragment),
                    typeof (WeekliesViewModel))
                };

                viewPager.Adapter = new MvxFragmentPagerAdapter(Activity, ChildFragmentManager, fragments);
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            return view;
        }
    }
}