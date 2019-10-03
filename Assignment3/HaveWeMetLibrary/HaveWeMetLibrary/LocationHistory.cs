using System;
using System.Collections.Generic;

namespace HaveWeMetLibrary
{
        public class LocationHistoryStruc
        {
            public List<LocationData> locations { get; set; }

            public class LocationData
            {
                public string timestampMs { get; set; }
                public int latitudeE7 { get; set; }
                public int longitudeE7 { get; set; }
                public int accuracy { get; set; }
                public List<Activity> activity { get; set; }
            }

            public class Activity
            {
                public string timestampMs { get; set; }
                public List<ActivityInfo> activity { get; set; }
            }

            public class ActivityInfo
            {
                public string type { get; set; }
                public int confidence { get; set; }
            }
        }

}
