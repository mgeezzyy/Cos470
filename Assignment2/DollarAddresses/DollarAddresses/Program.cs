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
    class DollarAddress
    {
        static void Main(string[] args)
        {
            DollarAddress app = new DollarAddress();
            String url_config = "C:\\Users\\windows_fausto\\Desktop\\cos470\\ass0\\Cos470\\Assignment2\\DollarAddresses\\appsettings.json";
            String address = app.CreateQuery(url_config);
            String json_results = app.GetSerializedRecords(address);
            var recs = JsonConvert.DeserializeObject<Address_records>(json_results);
            foreach(var r in recs.features)
            {
                Console.WriteLine(r.attributes.STREETNAME);
            }

        }

        public String CreateQuery(String url_config)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(url_config, false, true)
                .Build();

            var address = @"https://gis.maine.gov/arcgis/rest/services/Location/Maine_E911_Addresses_Roads_PSAP/MapServer/1/query?where=MUNICIPALITY%3D%27" 
                + config["location"] + "%27&outFields=ADDRESS_NUMBER%2CSTREETNAME%2CSUFFIX%2CMUNICIPALITY&resultRecordCount=" + config["recordCount"] + "&f=" + config["format"];

            return address;
        }

        public String GetSerializedRecords(String address)
        {
           var client = new WebClient();
           var content = client.DownloadString(address);
           return content;
        }

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


