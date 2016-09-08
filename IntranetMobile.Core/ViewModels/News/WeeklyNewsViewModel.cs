using System;
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
        private WeeklyItemViewModel _selectedItem;

        public WeeklyNewsViewModel()
        {
            Title = "Weeklies";
            SelectItem = new MvxCommand<WeeklyItemViewModel>(item => { SelectedItem = item; });
            ReloadCommand = new MvxCommand(async () =>
            {
                IsRefreshing = true;
                await ReloadData();
                IsRefreshing = false;
            });

            Task.Run(ReloadData);
        }

        public ObservableCollection<WeeklyItemViewModel> News { set; get; } =
            new ObservableCollection<WeeklyItemViewModel>();

        public WeeklyItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem != null)
                    ShowViewModel<WeekliesDetailsViewModel>(new WeekliesDetailsViewModel.Parameters { WeekliesId = _selectedItem.WeeklyId});

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

        public ICommand ReloadCommand { get; private set; }

        public virtual async Task ReloadData()
        {
            try
            {
                var allNews = await ServiceBus.NewsService.GetWeeklyNewsAsync(0, 15);

                InvokeOnMainThread(News.Clear);
                foreach (var item in allNews)
                {
                    InvokeOnMainThread(async () => { News.Add(await WeeklyItemViewModel.FromModel(item)); });
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}