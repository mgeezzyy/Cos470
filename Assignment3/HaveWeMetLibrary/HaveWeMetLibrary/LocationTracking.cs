using System;
using System.Collections.Generic;
using System.Text;

namespace HaveWeMetLibrary
{
    class LocationTracking
    {
        public static void WhereWasI(LocationHistoryStruc locationHistory, DateTime date)
        {
            foreach (var location in locationHistory.locations)
            {
                var date_buffer = DateTimeHelpers.GetDateFromTimeStamp(location.timestampMs);
                if (DateTime.Compare(date, date_buffer) == 0)
                    Console.WriteLine("On " + date + " your latitude was [" + location.latitudeE7
                     + "] and your longitude was [" + location.longitudeE7 + "]");
                else
                    Console.WriteLine("We couldn't find you for the date: " + date);
            }
        }
    }
}
