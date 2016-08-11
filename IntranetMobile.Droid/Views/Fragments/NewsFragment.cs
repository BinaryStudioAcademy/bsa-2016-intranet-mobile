using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Fragments;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.NewsFragment")]
    public class NewsFragment : BaseDrawerFragment<NewsFragmentViewModel>
    {
        public override int FragmentLayout { get; } = Resource.Layout.fragment_news;
        public override string Title { get; } = "Binary studio";
        public override string Subtitle { get; } = "Fancy subheader";
    }
}