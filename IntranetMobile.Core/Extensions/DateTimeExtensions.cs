using System;

namespace IntranetMobile.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimestampToDateTime(this long timestamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dateTime;
        }

        public static string ToDateTimeString(this DateTime date)
        {
            return date == DateTime.MinValue
                       ? ""
                       : date.ToString("dd MMM yyyy  HH:mm");
        }
    }
}
