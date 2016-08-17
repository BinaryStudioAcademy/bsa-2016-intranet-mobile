namespace IntranetMobile.Core.ViewModels.News
{
    public class AllNewsViewModel : BaseViewModel
    {
        public AllNewsViewModel()
        {
            Recycler = new CompanyNewsViewModel();
        }

        public CompanyNewsViewModel Recycler { get; private set; }
    }
}