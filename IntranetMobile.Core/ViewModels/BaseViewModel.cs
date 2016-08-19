using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private string _subtitle;
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set
            {
                _subtitle = value;
                RaisePropertyChanged(() => Subtitle);
            }
        }

        /// <summary>
        /// Resume this instance. Called when you come back from another ViewModel
        /// </summary>
        public virtual void Resume()
        {
        }
    }
}