using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private string _subtitle = string.Empty;
        private string _title = string.Empty;
        private bool _isBusy;

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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        ///     Resume this instance. Called when you come back from another ViewModel
        /// </summary>
        public virtual void Resume()
        {
        }

        /// <summary>
        ///     Pause this instance. Called when you gonna show another ViewModel and this ViewModel will be moved to the
        ///     background
        /// </summary>
        public virtual void Pause()
        {
        }
    }
}