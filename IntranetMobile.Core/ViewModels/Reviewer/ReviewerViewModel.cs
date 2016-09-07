using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerViewModel : BaseViewModel
    {
        private bool _isFilterActive;

        public ReviewerViewModel()
        {
            Title = "Reviewer";
        }

        public bool IsFilterActive
        {
            get { return _isFilterActive; }
            set
            {
                _isFilterActive = value;
                DotNet.IsCurrentUserFilterOn = IsFilterActive;
                JavaScript.IsCurrentUserFilterOn = IsFilterActive;
                Php.IsCurrentUserFilterOn = IsFilterActive;
            }
        }

        public ReviewerSectionViewModel DotNet { get; } = new ReviewerSectionViewModel(ReviewerGroup.DotNet);
        public ReviewerSectionViewModel JavaScript { get; } = new ReviewerSectionViewModel(ReviewerGroup.JavaScript);
        public ReviewerSectionViewModel Php { get; } = new ReviewerSectionViewModel(ReviewerGroup.Php);
    }
}
