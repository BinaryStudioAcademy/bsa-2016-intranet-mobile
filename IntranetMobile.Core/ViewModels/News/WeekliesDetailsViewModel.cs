using System.Collections.ObjectModel;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeekliesDetailsViewModel : BaseViewModel
    {
        private WeeklyNews _dataModel;

        public ObservableCollection<NewsDetailsViewModel> News { set; get; } =
            new ObservableCollection<NewsDetailsViewModel>();

        public async void Init(Parameters arg)
        {
            try
            {
                _dataModel = await ServiceBus.NewsService.GetWeeklyNewsByIdAsync(arg.WeekliesId);
                foreach (var fullNews in _dataModel.FullNews)
                {
                    var newDeatilsViewModel = new NewsDetailsViewModel();
                    newDeatilsViewModel.Init(new NewsDetailsViewModel.Parameters { NewsId = fullNews });
                    newDeatilsViewModel.NotifyHideAllNews = HideAllVms;
                    News.Add(newDeatilsViewModel);
                }
                Title = _dataModel.Title;
            }
            catch
            {
            }

        }

        public class Parameters
        {
            public string WeekliesId { get; set; }
        }

        private void HideAllVms()
        {
            foreach (var newsDetailsViewModel in News)
            {
                newsDetailsViewModel.Visibility = false;
            }
        }
    }
}