namespace IntranetMobile.Core.ViewModels.News
{
    public class AllNewsViewModel : BaseViewModel
    {
        public AllNewsViewModel()
        {
            Recycler = new NewsViewModel();
        }

        public NewsViewModel Recycler { get; private set; }
    }
}