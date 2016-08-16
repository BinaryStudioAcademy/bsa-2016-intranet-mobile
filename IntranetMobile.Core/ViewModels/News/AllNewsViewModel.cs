namespace IntranetMobile.Core.ViewModels.News
{
    public class AllNewsViewModel : BaseViewModel
    {
        public AllNewsViewModel()
        {
            Recycler = new CompanyViewModel();
        }

        public CompanyViewModel Recycler { get; private set; }
    }
}