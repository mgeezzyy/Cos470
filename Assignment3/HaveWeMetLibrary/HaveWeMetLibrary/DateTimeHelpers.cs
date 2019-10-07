using System;
using System.Collections.Generic;
using System.Text;


namespace HaveWeMetLibrary
{
    public class DateTimeHelpers
    {
        public static DateTime GetDateFromTimeStamp(string timestampMs)
        {
            var date = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(double.Parse(timestampMs));
            var newDate = date.AddMilliseconds(-date.Millisecond);
            return newDate;
        }

        public static bool TimesCollideMinute(string time1, string time2)
        {
            DateTime dt1 = DateTimeHelpers.GetDateFromTimeStamp(time1);
            DateTime dt2 = DateTimeHelpers.GetDateFromTimeStamp(time2);

            return dt1.CompareTo(dt2) == 0;
        }
    }
}
