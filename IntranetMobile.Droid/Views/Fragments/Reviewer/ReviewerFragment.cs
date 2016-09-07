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
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
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
                var reviewerRecycleFragmentType = typeof(ReviewerRecycleFragment);
                var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>
                {
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo(".NET",
                        reviewerRecycleFragmentType,
                        ViewModel.DotNet),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("JS",
                        reviewerRecycleFragmentType,
                        ViewModel.JavaScript),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("PHP",
                        reviewerRecycleFragmentType,
                        ViewModel.Php)
                };
                viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            HasOptionsMenu = true;
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_reviewer, menu);
            var bindingSet = this.CreateBindingSet<ReviewerFragment, ReviewerViewModel>();
            var a = menu.FindItem(Resource.Id.menu_item_reviewer);
            bindingSet.Bind(a.ActionView.FindViewById<SwitchCompat>(Resource.Id.filter_tickets_switch))
                .For("Checked")
                .To(viewModel => viewModel.IsFilterActive)
                .Mode(MvxBindingMode.TwoWay);
            bindingSet.Apply();
            base.OnCreateOptionsMenu(menu, inflater);
        }
    }
}