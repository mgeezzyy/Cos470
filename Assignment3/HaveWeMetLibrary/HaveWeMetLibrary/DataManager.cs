using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HaveWeMetLibrary
{
    public class DataManager
    {
        public static LocationHistoryStruc GetLocationHistoryStruc(string path)
        {
            return JsonConvert.DeserializeObject<LocationHistoryStruc>(File.ReadAllText(path));
        }
    }
}
