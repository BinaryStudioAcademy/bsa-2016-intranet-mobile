using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.NewsFragment")]
    public class NewsFragment : BaseDrawerFragment<NewsViewModel>
    {
        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_news;

        public override string ToolbarTitle { get; protected set; } = "Binary studio";

        public override string ToolbarSubtitle { get; protected set; } = "Fancy subheader";
    }
}