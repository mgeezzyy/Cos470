using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace DollarAddresses
{
    public class DollarAddress
    {
        private IConfigurationRoot Settings;

        static void Main(string[] args)
        {
            RunApp();
        }

        public static void RunApp()
        {
            String url_config = "C:\\Users\\windows_fausto\\Desktop\\cos470\\ass0\\Cos470\\Assignment2\\DollarAddresses\\appsettings.json";
            DollarAddress app = new DollarAddress();
            app.SetUpSettings(url_config);
            if (app.Settings["format"] != "pjson")
                Environment.Exit(0);

            String address = app.CreateQuery();
            String json_results = app.GetSerializedRecords(address);
            var recs = JsonConvert.DeserializeObject<Address_records>(json_results);
            foreach (var r in recs.features)
            {
                if (app.IsItDollarAddress(r.attributes))
                    Console.WriteLine(r.attributes.ADDRESS_NUMBER + " " + r.attributes.STREETNAME + " " + r.attributes.SUFFIX);
            }
        }

        /* Reads config file and sets up settings for app */
        public void SetUpSettings(String url_config)
        {
            Settings = new ConfigurationBuilder()
                .AddJsonFile(url_config, false, true)
                .Build();
        }

        /* creates a query using the settings */
        public String CreateQuery()
        {
            var address = @"https://gis.maine.gov/arcgis/rest/services/Location/Maine_E911_Addresses_Roads_PSAP/MapServer/1/query?where=MUNICIPALITY%3D%27" 
                + Settings["location"] + "%27&outFields=ADDRESS_NUMBER%2CSTREETNAME%2CSUFFIX%2CMUNICIPALITY&resultRecordCount=" + Settings["recordCount"] + "&f=" + Settings["format"];

            return address;
        }

        /* Returns string with serialized records */ 
        public String GetSerializedRecords(String address)
        {
           var client = new WebClient();
           var content = client.DownloadString(address);
           return content;
        }

        /* returns true if the address is a dollar address */
        public bool IsItDollarAddress(Address_records.Address address)
        {
            String street_name = address.STREETNAME;
            String suffix = address.SUFFIX;
            String address_ltrs = street_name + suffix;
            int addr_number = address.ADDRESS_NUMBER;
            if (addr_number == CalculateValue(address_ltrs))
                return true;

            return false;
        }

        /* calculates value of string */
        public int CalculateValue(String name)
        {
            int total = 0;
            name = name.ToLower();

            foreach (char C in name)
                total += MapCharToValue(C);

            return total;
        }

        /* Maps A-Za-z characters to integers. */
        public int MapCharToValue(char C)
        {
            if (C >= 'a' && C <= 'z')
                return C - 'a' + 1;

            return 0;
        }

        /* classes used to deserialize records */
        public class Address_records
        {
            public String displayFieldName { get; set; }
            public FieldAliases fieldAliases { get; set; }
            public List<Field> fields { get; set; }
            public List<Attribute> features { get; set; }

            public class FieldAliases
            {
                public String ADDRESS_NUMBER { get; set; }
                public String STREETNAME { get; set; }
                public String SUFFIX { get; set; }
                public String Municipality { get; set; }
                public String Latitude { get; set; }
                public String Longtitude { get; set; }
            }

            public class Field
            {
                public String name { get; set; }
                public String type { get; set; }
                public String alias { get; set; }
                public int length { get; set; }
            }
            public class Attribute
            {
                public Address attributes { get; set; }
            }
            public class Address
            {
                public int ADDRESS_NUMBER { get; set; }
                public String STREETNAME { get; set; }
                public String SUFFIX { get; set; }
                public String MUNICIPALITY { get; set; }
            }
        }
    }
}


