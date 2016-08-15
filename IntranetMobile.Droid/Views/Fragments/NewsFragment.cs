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
        public NewsFragment()
        {
            FragmentLayout = Resource.Layout.fragment_news;
            Title = "Binary studio";
            Subtitle = "Fancy subheader";
        }
    }
}