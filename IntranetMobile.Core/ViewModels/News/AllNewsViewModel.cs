namespace IntranetMobile.Core.ViewModels.News
{
    public class AllNewsViewModel : BaseViewModel
    {
        public AllNewsViewModel()
        {
            Title = "News";

            CompanyNews = new CompanyNewsViewModel();
            WeeklyNews = new WeeklyNewsViewModel();
        }

        public CompanyNewsViewModel CompanyNews { get; private set; }

        public WeeklyNewsViewModel WeeklyNews { get; private set; }
    }
}