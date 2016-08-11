using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        private ObservableCollection<ItemNewsViewModel> listNews = new ObservableCollection<ItemNewsViewModel>()
        {
            new ItemNewsViewModel() {Title = "New1", SubTitle = "Author111"},
            new ItemNewsViewModel() {Title = "New2", SubTitle = "Author222"},
            new ItemNewsViewModel() {Title = "New3", SubTitle = "Author333"}
        };

        private ItemNewsViewModel selectedItem;

        public NewsViewModel()
        {
            
        }

        public ObservableCollection<ItemNewsViewModel> ListNews
        {
            set { listNews = value; }
            get { return listNews; }
        }

        public ItemNewsViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem
        {
            get
            {
                return new MvxCommand<ItemNewsViewModel>(item =>
                {
                    SelectedItem = item;
                });
            }

        }
    }
}