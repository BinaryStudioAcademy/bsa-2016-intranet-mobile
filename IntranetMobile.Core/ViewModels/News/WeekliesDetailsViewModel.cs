using System;
using System.Collections.ObjectModel;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeekliesDetailsViewModel : BaseViewModel
    {
        private string _body;
        private WeeklyNews _dataModel;

        public ObservableCollection<NewsDetailsViewModel> News { set; get; } =
            new ObservableCollection<NewsDetailsViewModel>();

        public async void Init(Parameters arg)
        {
            try
            {
                _dataModel = await ServiceBus.NewsService.GetWeeklyNewsByIdAsync(arg.WeekliesId);
                Body = "";
                foreach (var fullNewsId in _dataModel.FullNews)
                {
                    var newsItem = await ServiceBus.NewsService.GetNewsByIdAsync(fullNewsId);
                    if (newsItem != null)
                    {
                        var header = $"<h3>{newsItem.Title}</h3>";
                        var footer = "<br/>";
                        Body += $"{header} {newsItem.Body} {footer}";
                    }
                    //var newDeatilsViewModel = new NewsDetailsViewModel();
                    //newDeatilsViewModel.Init(new NewsDetailsViewModel.Parameters { NewsId = fullNews });
                    //News.Add(newDeatilsViewModel);
                }
                Title = _dataModel.Title;
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
        }

        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                RaisePropertyChanged(() => Body);
            }
        }

        public class Parameters
        {
            public string WeekliesId { get; set; }
        }
    }
}