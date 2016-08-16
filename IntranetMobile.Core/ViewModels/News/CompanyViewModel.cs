using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CompanyViewModel : BaseNewsViewModel
    {
        private NewsPreviewViewModel _selectedItem;

        public CompanyViewModel()
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
        private bool _isRefreshing;

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    IsRefreshing = true;

                    await ReloadData();

                    IsRefreshing = false;
                });
            }
        }

        public virtual async Task ReloadData()
        {
            
        }
    }
}