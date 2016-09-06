using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerViewModel : BaseViewModel
    {
        public ReviewerViewModel()
        {
            Title = "Reviewer";
        }

        public ReviewerSectionViewModel DotNet { get; } = new ReviewerSectionViewModel(ReviewerGroup.DotNet);
        public ReviewerSectionViewModel JavaScript { get; } = new ReviewerSectionViewModel(ReviewerGroup.JavaScript);
        public ReviewerSectionViewModel Php { get; } = new ReviewerSectionViewModel(ReviewerGroup.Php);
    }
}
