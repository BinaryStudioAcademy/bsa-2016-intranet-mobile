using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeeklyNewsViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private NewsItemViewModel _selectedItem;

        public WeeklyNewsViewModel()
        {
            Title = "Weeklies";
            SelectItem = new MvxCommand<NewsItemViewModel>(item => { SelectedItem = item; });
            Task.Run(ReloadData);
        }

        public ObservableCollection<WeeklyItemViewModel> News { set; get; } =
            new ObservableCollection<WeeklyItemViewModel>();

        public NewsItemViewModel SelectedItem
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
            var allNews = await ServiceBus.NewsService.GetWeeklyNews(0, 10);

            InvokeOnMainThread(News.Clear);
            foreach (var item in allNews)
            {
                InvokeOnMainThread(async () => { News.Add(await WeeklyItemViewModel.FromModel(item)); });
            }
        }
    }
}