using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Text;


namespace HaveWeMetLibrary
{
    class DateTimeHelpers
    {
        public static DateTime GetDateFromTimeStamp(string timestampMs)
        {
            var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(timestampMs));
            var newDate = date.AddMilliseconds(-date.Millisecond);
            return newDate;
        }

        public static bool LocationsCollide(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            var coordinate1 = new GeoCoordinate(latitude1, longitude1);
            var coordinate2 = new GeoCoordinate(latitude2, longitude2);
            double distance = coordinate1.GetDistanceTo(coordinate2);
            double thresholdOfCloseness = 2.0;

            if (distance <= thresholdOfCloseness)
                return true;
            else
                return false;
        }
    }
}
