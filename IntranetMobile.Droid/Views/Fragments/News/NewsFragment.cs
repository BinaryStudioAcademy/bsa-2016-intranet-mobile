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
        public NewsFragment()
        {
            FragmentLayout = Resource.Layout.fragment_news;
            ToolbarLayout = Resource.Id.tabbed_toolbar;
            Title = "Binary studio";
            Subtitle = "Fancy subheader";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxFragmentPagerAdapter.FragmentInfo>
                {
                    new MvxFragmentPagerAdapter.FragmentInfo("COMPANY NEWS", typeof (NewsRecyclerViewFragment),
                        typeof (NewsViewModel)),
                    new MvxFragmentPagerAdapter.FragmentInfo("WEEKLIES", typeof (NewsRecyclerViewFragment),
                    typeof (NewsViewModel))
                };
                viewPager.Adapter = new MvxFragmentPagerAdapter(Activity, ChildFragmentManager, fragments);
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            return view;
        }
    }
}