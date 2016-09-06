using System;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class BaseItemReviewViewModel : BaseViewModel
    {
        private string _author;
        private string _authorImage;
        private string _id;
        private string _reviewerText;
        public ICommand ClickViewDetailsCommand { get; set; }
        public int VmId { get; set; }

        protected DateTime dateTime;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public string AuthorImage
        {
            get { return _authorImage; }
            set
            {
                _authorImage = value;
                RaisePropertyChanged(() => AuthorImage);
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
            get { return dateTime.ToDateTimeString(); }
        }

        public string ReviewText
        {
            get { return _reviewerText; }
            set
            {
                _reviewerText = value;
                RaisePropertyChanged(() => ReviewText);
            }
        }
    }
}