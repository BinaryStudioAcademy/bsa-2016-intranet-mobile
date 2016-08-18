using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CompanyNewsViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private NewsViewModel _selectedItem;

        public CompanyNewsViewModel()
        {
            Title = "Company";
            SelectItem = new MvxCommand<NewsViewModel>(item => { SelectedItem = item; });
            Task.Run(ReloadData);
        }

        public ObservableCollection<NewsViewModel> News { set; get; } =
            new ObservableCollection<NewsViewModel>();

        public NewsViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ShowViewModel<NewsDetailsViewModel>(new NewsDetailsViewModel.Parameters {NewsId = _selectedItem.NewsId});

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem { get; private set; }

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
            // TODO: Normalize news pull
            var allNews = await ServiceBus.NewsService.GetCompanyNews(0, 10);

            InvokeOnMainThread(News.Clear);
            foreach (var news in allNews)
            {
                InvokeOnMainThread(() => { News.Add(NewsViewModel.FromModel(news)); });
            }
        }
    }
}