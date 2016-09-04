using System;
using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Reviewer;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace IntranetMobile.Droid.Views.Fragments.Reviewer
{
    [MvxFragment(typeof (MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.reviewer.ReviewerRecycleFragment")]
    public class ReviewerRecycleFragment : BaseFragment<ReviewerSectionViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.recycle_view_reviewer, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.reviewer_recycler_view);
            recyclerView.ItemTemplateSelector = new ReviewTypeSelector();


            var swipeToRefresh = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresher);
            var appBar = Activity.FindViewById<AppBarLayout>(Resource.Id.appbar);
            if (appBar != null)
                appBar.OffsetChanged += (sender, args) => swipeToRefresh.Enabled = args.VerticalOffset == 0;

            return view;
        }
    }

    public class ReviewTypeSelector : IMvxTemplateSelector
    {
        private readonly Dictionary<Type, int> _typeMapping;

        public ReviewTypeSelector()
        {
            _typeMapping = new Dictionary<Type, int>
            {
                {typeof (ItemUserReviewViewModel), Resource.Layout.card_view_user_reviewer},
                {typeof (ItemReviewViewModel), Resource.Layout.card_view_reviewer}
            };
        }

        public int GetItemViewType(object forItemObject)
        {
            return _typeMapping[forItemObject.GetType()];
        }

        public int GetItemLayoutId(int fromViewType)
        {
            return fromViewType;
        }
    }
}