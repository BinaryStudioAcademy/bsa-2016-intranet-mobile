using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Reviewer;
using IntranetMobile.Droid.Views.Fragments.News;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IntranetMobile.Droid.Views.Fragments.Reviewer
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.reviewer.ReviewerFragment")]
    public class ReviewerFragment : BaseDrawerFragment<ReviewerViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.tabbed_toolbar;
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_reviewer;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.reviewer_viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>
                {
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("C#",
                        typeof(ReviewerRecycleFragment),
                        ViewModel.Cs),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("PHP",
                        typeof(ReviewerRecycleFragment),
                        ViewModel.Js),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("JS",
                        typeof(ReviewerRecycleFragment),
                        ViewModel.Php)
                };
                viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            return view;
        }
    }
}