using System.Collections.Generic;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeeklyDetailsViewModel : BaseViewModel
    {
        private WeeklyNews _dataModel;

        public WeeklyDetailsViewModel()
        {
        }

        public async void Init(Parameters arg)
        {
            _dataModel = ServiceBus.NewsService.GetWeeklyNewsByIdAsync(arg.NewsId);

            Title = _dataModel.Title;
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}