using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemReviewViewModel : BaseViewModel
    {
        private string _author;
        private string _authorImage;
        private string _dateTime;
        private string _reviewerText;
        private bool _signed;
        private string _titleName;

        public ItemReviewViewModel()
        {
            ClickViewDetailsCommand = new MvxCommand(ClickViewDetailsCommandExecute);
        }

        public ICommand ClickViewDetailsCommand { get; private set; }

        public string AuthorImage
        {
            get { return _authorImage; }
            set
            {
                _authorImage = value;
                RaisePropertyChanged(() => AuthorImage);
            }
        }

        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                RaisePropertyChanged(() => TitleName);
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged(() => Author);
            }
        }

        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                RaisePropertyChanged(() => DateTime);
            }
        }

        public bool Signed
        {
            get { return _signed; }
            set
            {
                _signed = value;
                RaisePropertyChanged(() => Signed);
            }
        }

        public string ReviewerText
        {
            get { return _reviewerText; }
            set
            {
                _reviewerText = value;
                RaisePropertyChanged(() => ReviewerText);
            }
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
            };
        }
    }
}