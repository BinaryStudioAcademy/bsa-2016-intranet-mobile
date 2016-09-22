using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerViewModel : BaseViewModel
    {
        private bool _isFilterActive;

        public ReviewerViewModel()
        {
            CreateNewTicketCommand = new MvxCommand(CreateNewTicketCommandExecute);
            Title = "Reviewer";
        }

        private void CreateNewTicketCommandExecute()
        {
            ShowViewModel<NewTicketViewModel>();
        }

        public bool IsFilterActive
        {
            get { return _isFilterActive; }
            set
            {
                _isFilterActive = value;
                DotNet.IsCurrentUserFilterOn = _isFilterActive;
                JavaScript.IsCurrentUserFilterOn = _isFilterActive;
                Php.IsCurrentUserFilterOn = _isFilterActive;
            }
        }

        public ICommand CreateNewTicketCommand { get; set; }
        public ReviewerSectionViewModel DotNet { get; } = new ReviewerSectionViewModel(ReviewerGroup.DotNet);
        public ReviewerSectionViewModel JavaScript { get; } = new ReviewerSectionViewModel(ReviewerGroup.JavaScript);
        public ReviewerSectionViewModel Php { get; } = new ReviewerSectionViewModel(ReviewerGroup.Php);
    }
}
