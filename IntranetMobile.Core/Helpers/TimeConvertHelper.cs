using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Helpers
{
    public class TimeConvertHelper
    {
        public static DateTime ConvertFromUnixTimestamp(double timestampMs)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddMilliseconds(timestampMs);
        }
    }
}
