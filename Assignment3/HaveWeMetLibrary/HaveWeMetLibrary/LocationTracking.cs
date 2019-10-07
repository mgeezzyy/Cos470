using System;
using System.Collections.Generic;
using System.Text;
using GeoCoordinatePortable;


namespace HaveWeMetLibrary
{
    public class LocationTracking
    {
        public static Location WhereWasI(LocationHistoryStruc locationHistory, String timeStamp)
        {
            Location result = new Location();
            DateTime date = DateTimeHelpers.GetDateFromTimeStamp(timeStamp);

            foreach (var location in locationHistory.locations)
            {
                var date_buffer = DateTimeHelpers.GetDateFromTimeStamp(location.timestampMs);
                if (DateTime.Compare(date, date_buffer) == 0)
                {
                    result.Latitude = location.latitudeE7;
                    result.Longitude = location.longitudeE7;
                    result.TimeStamp = timeStamp;
                    return result;
                }
            }
       
            return null;
        }

        public static bool LocationsCollide(Location location1, Location location2, double withInMeters)
        {
            var coordinate1 = new GeoCoordinate(location1.Latitude / 1e7, location1.Longitude / 1e7);
            var coordinate2 = new GeoCoordinate(location2.Latitude / 1e7, location2.Longitude / 1e7);
            double distance = coordinate1.GetDistanceTo(coordinate2);

            if (distance <= withInMeters && DateTimeHelpers.TimesCollideMinute(location1.TimeStamp, location2.TimeStamp))
                return true;
            else
                return false;
        }

        public class Location
        {
            private string timeStamp;
            private int latitude;
            private int longitude;

            public string TimeStamp
            {
                get { return timeStamp; }
                set { timeStamp = value; }
            }

            public DateTime Date
            {
                get { return DateTimeHelpers.GetDateFromTimeStamp(timeStamp); }
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
