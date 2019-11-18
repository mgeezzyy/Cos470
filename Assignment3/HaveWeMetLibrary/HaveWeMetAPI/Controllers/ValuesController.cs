using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaveWeMetLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HaveWeMetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static Dictionary<string, LocationHistoryStruc> LocHistoryOfPeople = new Dictionary<string, LocationHistoryStruc>();
        public IConfiguration Settings = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();

        public ValuesController()
        {
            var fpath = Settings["DataPath_VeryShort"];
            var history1 = DataManager.GetLocationHistoryStruc(fpath);
            if (!LocHistoryOfPeople.ContainsKey("Sancho"))
                LocHistoryOfPeople.Add("Sancho", history1); 

        }

        // GET api/values
        [HttpGet]
        public ActionResult<Dictionary<String, LocationHistoryStruc>> Get()
        {
            return LocHistoryOfPeople;
        }

        // GET api/values/name
        [HttpGet("{name}")]
        public ActionResult<LocationHistoryStruc> Get(string name)
        {
            if (LocHistoryOfPeople.ContainsKey(name))
                return LocHistoryOfPeople[name];
            return NotFound("Sorry! Location history for " + name + " was not found!");
        }

        // GET api/values/name/date
        [HttpGet("{name}/{date}")]
        public ActionResult<string> Get(string name, DateTime date)
        {
            if (LocHistoryOfPeople.ContainsKey(name))
            {
                LocationTracking.Location location = LocationTracking.WhereWasI(LocHistoryOfPeople[name], date);
                if (location == null)
                    return NotFound("No location found for date: " + date);

                return "On the date " + date + "," + name + " was at latitude-" + location.Latitude + " and longitude-" + location.Longitude;
            }
            return NotFound("Sorry! Location history for " + name + " was not found!");
        }

        // PUT api/post/name
        [HttpPost("post/{name}")]
        public ActionResult<bool> Post([FromBody] string locHistoryStr, string name)
        {
            if (LocHistoryOfPeople.ContainsKey(name))
                return false;

            var history = JsonConvert.DeserializeObject<LocationHistoryStruc>(name);
            LocHistoryOfPeople.Add(name, history);
            return true;
        }

        // DELETE api/delete/name
        [HttpDelete("delete/{name}")]
        public ActionResult<bool> Delete(string name)
        {
            if (!LocHistoryOfPeople.ContainsKey(name))
                return false;

            LocHistoryOfPeople.Remove(name);
            return true;
        }
    }
}
