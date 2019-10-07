using System;
using System.Collections.Generic;
using System.Text;

namespace HaveWeMetLibrary
{
    class LocationTracking
    {
        public static Location WhereWasI(LocationHistoryStruc locationHistory, DateTime date)
        {
            Location result = new Location();
            foreach (var location in locationHistory.locations)
            {
                var date_buffer = DateTimeHelpers.GetDateFromTimeStamp(location.timestampMs);
                if (DateTime.Compare(date, date_buffer) == 0)
                {
                    result.Latitude = location.latitudeE7;
                    result.Longitude = location.longitudeE7;
                    result.Date = date;
                    return result;
                }
            }
            return null;
        }

        public class Location
        {
            private string timeStamp;
            private DateTime date;
            private int latitude;
            private int longitude;

            public string TimeStamp
            {
                get { return timeStamp; }
                set { timeStamp = value; }
            }

            public DateTime Date
            {
                get { return date; }
                set { date = value; }
            }

            public int Latitude
            {
                get { return latitude; }
                set { latitude = value; }
            }

            public int Longitude
            {
                get { return longitude; }
                set { longitude = value; }
            }
        }
    }
}
