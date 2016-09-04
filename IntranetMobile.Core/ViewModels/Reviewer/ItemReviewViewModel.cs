using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemReviewViewModel : BaseItemReviewViewModel
    {
        private string _buttonText;
        private bool _isSigned;

        public ItemReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
            ClickSignCommand = new MvxCommand(ClickSignCommandxecute);
        }

        public ICommand ClickSignCommand { get; set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                RaisePropertyChanged(() => ButtonText);
            }
        }

        public bool IsSigned
        {
            get { return _isSigned; }
            set
            {
                _isSigned = value;
                ButtonText = _isSigned ? "Undo" : "Join";
            }
        }

        private void ClickSignCommandxecute()
        {
            IsSigned = !_isSigned;
        }

        private void ClickViewDetailsCommandExecute()
        {
            ShowViewModel<TicketDetailsViewModel>();
        }

        public static ItemReviewViewModel GetItemReviewViewModelFromDto(TicketsDto dto)
        {
            return new ItemReviewViewModel
            {
                AuthorImage = Constants.BaseUrl + dto.user.avatar,
                Author = $"{dto.user.first_name} {dto.user.last_name}",
                DateTime = dto.date_review,
                ReviewerText = dto.details,
                TitleName = dto.title
                //IsSigned need responce from server to know is user signed or not
            };
        }
    }
}