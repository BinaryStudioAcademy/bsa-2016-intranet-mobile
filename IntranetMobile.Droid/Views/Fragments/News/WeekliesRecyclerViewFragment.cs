using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IntranetMobile.Droid.Views.Fragments.News
{

    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("intranetmobile.droid.views.fragments.news.WeekliesRecyclerViewFragment")]
    public class WeekliesRecyclerViewFragment : MvxFragment<WeeklyNewsViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.recycle_view_weekly, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            var swipeToRefresh = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresher);
            var appBar = Activity.FindViewById<AppBarLayout>(Resource.Id.appbar);
            if (appBar != null)
                appBar.OffsetChanged += (sender, args) => swipeToRefresh.Enabled = args.VerticalOffset == 0;

            return view;
        }
    }
}