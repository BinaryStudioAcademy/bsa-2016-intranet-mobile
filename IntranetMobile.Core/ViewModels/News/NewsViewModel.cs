using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private ItemNewsViewModel _selectedItem;

        public NewsViewModel()
        {
            ListNews = new ObservableCollection<ItemNewsViewModel>
            {
                new ItemNewsViewModel {Title = "New1", Subtitle = "Author111"},
                new ItemNewsViewModel {Title = "New2", Subtitle = "Author222"},
                new ItemNewsViewModel {Title = "New3", Subtitle = "Author333"}
            };
        }

        public ObservableCollection<ItemNewsViewModel> ListNews { set; get; }

        public ItemNewsViewModel SelectedItem
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
            get { return new MvxCommand<ItemNewsViewModel>(item => { SelectedItem = item; }); }
        }
    }
}