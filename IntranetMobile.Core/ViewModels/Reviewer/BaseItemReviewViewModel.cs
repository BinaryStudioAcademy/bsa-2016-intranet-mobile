using System.Windows.Input;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class BaseItemReviewViewModel : BaseViewModel
    {
        private string _author;
        private string _authorImage;
        private string _dateTime;
        private string _reviewerText;
        private string _titleName;
        public ICommand ClickViewDetailsCommand { get; set; }


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

        public string ReviewerText
        {
            get { return _reviewerText; }
            set
            {
                _reviewerText = value;
                RaisePropertyChanged(() => ReviewerText);
            }
        }
    }
}