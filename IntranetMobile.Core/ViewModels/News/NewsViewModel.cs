using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private NewsPreviewViewModel _selectedItem;

        public NewsViewModel()
        {
            ListNews = new ObservableCollection<NewsPreviewViewModel>
            {
                new NewsPreviewViewModel {Title = "New1", Subtitle = "Author111"},
                new NewsPreviewViewModel {Title = "New2", Subtitle = "Author222"},
                new NewsPreviewViewModel {Title = "New3", Subtitle = "Author333"}
            };
        }

        public ObservableCollection<NewsPreviewViewModel> ListNews { set; get; }

        public NewsPreviewViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ShowViewModel<NewsDetailsViewModel>(SelectedItem);

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem
        {
            get { return new MvxCommand<NewsPreviewViewModel>(item => { SelectedItem = item; }); }
        }
    }
}