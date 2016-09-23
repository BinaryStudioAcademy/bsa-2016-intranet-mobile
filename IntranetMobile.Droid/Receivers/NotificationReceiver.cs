using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V7.App;
using Android.Util;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Droid.Views.Activities;

namespace IntranetMobile.Droid.Receivers
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] {"com.binarystudio.intranetmobile.CHECK_SERVER_RECORDS"})]
    public class NotificationReceiver : BroadcastReceiver
    {
        private const int CheckIntervalSec = 15;
        private const int NotificationId = 7;

        private static bool _isRunned;
        private static Settings _settings;
        private static Context _context;
        private static AlarmManager _alarmManager;
        private static PendingIntent _pendingIntent;

        private static DateTime _latestCompanyNewsDate;
        private static DateTime _latestWeeklyNewsDate;
        private static DateTime _latestReviewDate;

        public override void OnReceive(Context context, Intent intent)
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
                Log.Error("NotificationReceiver", ex.ToString());
            }
        }

        public static async void Run(Context context, Settings settings)
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

                _context = context;
                _alarmManager = _context.GetSystemService(Context.AlarmService) as AlarmManager;
                var intent = new Intent(_context, typeof(NotificationReceiver));
                _pendingIntent = PendingIntent.GetBroadcast(_context, 0, intent, PendingIntentFlags.UpdateCurrent);
                _alarmManager?.SetRepeating(AlarmType.RtcWakeup,
                    0,
                    1000*CheckIntervalSec,
                    _pendingIntent);
            }
            catch (Exception ex)
            {
                Log.Error("NotificationReceiver", ex.ToString());
            }
        }

        public static void Stop()
        {
            if (_alarmManager != null && _pendingIntent != null)
            {
                _alarmManager.Cancel(_pendingIntent);
                _isRunned = false;
            }
        }

        private async void CheckCompanyNews()
        {
            var latestNews = await ServiceBus.NewsService.GetCompanyNewsAsync(0, 2);
            if (latestNews != null && latestNews.Count > 0)
            {
                var lastDate = latestNews[0].Date;
                if (DateTime.Compare(_latestCompanyNewsDate, lastDate) < 0)
                {
                    _latestCompanyNewsDate = lastDate;
                    var notificationManager =
                        _context.GetSystemService(Context.NotificationService) as NotificationManager;
                    var notification = BuildNotification("We have company news for you", "news", latestNews[0].NewsId);
                    notificationManager?.Notify(NotificationId, notification);
                }
            }
        }

        private async void CheckWeeklyNews()
        {
            var latestWeeklies = await ServiceBus.NewsService.GetWeeklyNewsAsync(0, 2);
            if (latestWeeklies != null && latestWeeklies.Count > 0)
            {
                var lastDate = latestWeeklies[0].Date;
                if (DateTime.Compare(_latestWeeklyNewsDate, lastDate) < 0)
                {
                    _latestCompanyNewsDate = lastDate;
                    var notificationManager =
                        _context.GetSystemService(Context.NotificationService) as NotificationManager;
                    var notification = BuildNotification("We have weekly news for you", "weekly",
                        latestWeeklies[0].WeeklyId);
                    notificationManager?.Notify(NotificationId, notification);
                }
            }
        }

        private async void CheckReviewer()
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
                        var notificationManager =
                            _context.GetSystemService(Context.NotificationService) as NotificationManager;
                        var notification = BuildNotification("Reviewer contains new request", "reviewer",
                            request.TicketId);
                        notificationManager?.Notify(NotificationId, notification);
                    }
                }
            }
        }

        private Notification BuildNotification(string text, string extrasString, string itemId)
        {
            var mainActivityIntent = new Intent(_context, typeof(MainActivity));
            mainActivityIntent.PutExtra("current_fragment", extrasString);
            mainActivityIntent.PutExtra("item_id", itemId);
            var pendingIntent = PendingIntent.GetActivity(_context, 1, mainActivityIntent,
                PendingIntentFlags.UpdateCurrent);

            var notificationBuilder = new NotificationCompat.Builder(_context)
                .SetAutoCancel(true)
                .SetSmallIcon(Resource.Drawable.ic_notification)
                .SetContentTitle("Binary Studio")
                .SetContentText(text)
                .SetContentIntent(pendingIntent);

            if (_settings.IsVibrationEnabled)
            {
                notificationBuilder.SetVibrate(new long[] {400, 500, 400, 400});
            }

            notificationBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

            var notification = notificationBuilder.Build();
            return notification;
        }
    }
}