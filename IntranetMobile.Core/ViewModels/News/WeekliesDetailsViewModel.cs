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
            _dataModel = await ServiceBus.NewsService.GetWeeklyNewsByIdAsync(arg.WeekliesId);
            foreach (var fullNews in _dataModel.FullNews)
            {
                var newDeatilsViewModel = new NewsDetailsViewModel();
                newDeatilsViewModel.Init(new NewsDetailsViewModel.Parameters {NewsId = fullNews});

                News.Add(newDeatilsViewModel);
            }
            Title = _dataModel.Title;
        }

        public class Parameters
        {
            public string WeekliesId { get; set; }
        }
    }
}