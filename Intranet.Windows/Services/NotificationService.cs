using System;
using System.Collections.Generic;
using System.Linq;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace Intranet.WindowsUWP.Services
{
    public class NotificationService
    {
        public const int CheckIntervalSec = 15;

        private static bool _isRunned;
        private static Settings _settings;

        private static DateTime _latestCompanyNewsDate;
        private static DateTime _latestWeeklyNewsDate;
        private static DateTime _latestReviewDate;

        public static void CheckServerState()
        {
            try
            {
                if (ServiceBus.UserService.CurrentUser == null)
                    return;

                if (_settings.IsNewsNotificationEnabled)
                {
                    CheckCompanyNews();
                    CheckWeeklyNews();
                }

                if (_settings.IsReviewerNotificationEnabled)
                {
                    CheckReviewer();
                }
            }
            catch (Exception ex)
            {
                //Log.Error("NotificationReceiver", ex.ToString());
            }
        }

        public static async void Run(Settings settings)
        {
            if (_isRunned)
                return;

            try
            {
                _isRunned = true;
                _settings = settings;

                if (_settings.IsNewsNotificationEnabled)
                {
                    var news = await ServiceBus.NewsService.GetCompanyNewsAsync(0, 2);
                    if (news != null && news.Count > 0)
                        _latestCompanyNewsDate = news[0].Date;
                    else
                        _latestCompanyNewsDate = DateTime.MinValue;

                    var weeklies = await ServiceBus.NewsService.GetWeeklyNewsAsync(0, 2);
                    if (weeklies != null && weeklies.Count > 0)
                        _latestWeeklyNewsDate = weeklies[0].Date;
                    else
                        _latestWeeklyNewsDate = DateTime.MinValue;
                }

                if (_settings.IsReviewerNotificationEnabled)
                {
                    var allReview = await ServiceBus.ReviewerService.GetListOfTicketsAsync();
                    if (allReview != null)
                    {
                        var reviewer = allReview.Where(r => !r.UserServerId.
                            Equals(ServiceBus.UserService.CurrentUser.ServerId))
                            .OrderByDescending(i => i.DateReview);
                        _latestReviewDate = reviewer.Any() ? reviewer.First().DateReview : DateTime.MinValue;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error("NotificationReceiver", ex.ToString());
            }
        }
        
        private static async void CheckCompanyNews()
        {
            var latestNews = await ServiceBus.NewsService.GetCompanyNewsAsync(0, 2);
            if (latestNews != null && latestNews.Count > 0)
            {
                var lastDate = latestNews[0].Date;
                if (DateTime.Compare(_latestCompanyNewsDate, lastDate) < 0)
                {
                    _latestCompanyNewsDate = lastDate;
                    ServiceBus.AlertService.ShowPopupMessage("We have company news for you");
                }
            }
        }

        private static async void CheckWeeklyNews()
        {
            var latestWeeklies = await ServiceBus.NewsService.GetWeeklyNewsAsync(0, 2);
            if (latestWeeklies != null && latestWeeklies.Count > 0)
            {
                var lastDate = latestWeeklies[0].Date;
                if (DateTime.Compare(_latestWeeklyNewsDate, lastDate) < 0)
                {
                    _latestCompanyNewsDate = lastDate;
                    ServiceBus.AlertService.ShowPopupMessage("We have weekly news for you");
                }
            }
        }

        private static async void CheckReviewer()
        {
            var allReview = await ServiceBus.ReviewerService.GetListOfTicketsAsync();
            if (allReview != null)
            {
                var reviewer = allReview.Where(r => !r.UserServerId.
                    Equals(ServiceBus.UserService.CurrentUser.ServerId))
                    .OrderByDescending(i => i.DateReview);
                if (reviewer.Any())
                {
                    var request = reviewer.First();
                    var lastDate = request.DateReview;
                    if (DateTime.Compare(_latestReviewDate, lastDate) < 0)
                    {
                        _latestReviewDate = lastDate;
                        ServiceBus.AlertService.ShowPopupMessage("Reviewer contains new request");
                    }
                }
            }
        }
    }
}
