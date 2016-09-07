using System;
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
        private NewsItemViewModel _selectedItem;

        public CompanyNewsViewModel()
        {
            Title = "Company";
            SelectItem = new MvxCommand<NewsItemViewModel>(item => { SelectedItem = item; });
            ReloadCommand = new MvxCommand(async () =>
            {
                IsRefreshing = true;
                await ReloadData();
                IsRefreshing = false;
            });

            Task.Run(ReloadData);
        }

        public ObservableCollection<NewsItemViewModel> News { set; get; } =
            new ObservableCollection<NewsItemViewModel>();

        public NewsItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem != null)
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

        public ICommand ReloadCommand { get; private set; }

        public override void Resume()
        {
            base.Resume();

            if (News != null && News.Count > 0)
            foreach (var item in News)
                item.Resume();
        }

        public virtual async Task ReloadData()
        {
            try
            {
                var companyNews = await ServiceBus.NewsService.GetCompanyNewsAsync(0, 15);

                InvokeOnMainThread(News.Clear);
                foreach (var news in companyNews)
                {
                    InvokeOnMainThread(() => { News.Add(NewsItemViewModel.FromModel(news)); });
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}