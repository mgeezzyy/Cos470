using NUnit.Framework;
using HaveWeMetLibrary;
using System;
using static HaveWeMetLibrary.LocationTracking;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_DeserializeJSON_ReturnsCorrectType()
        {
            var path = "LocationHistoryVeryShort.json";
            var locationHistoryData = DataManager.GetLocationHistoryStruc(path);
            Assert.IsInstanceOf(typeof(LocationHistoryStruc), locationHistoryData);
        }

        [Test]
        public void Test_GetDateFromTimeStamp_EqualTrue()
        {
            DateTime date = new DateTime(2019, 10, 7, 18, 44, 48);
            string timeStamp = "1570473888556";  // 10/7/2019 timestamp
            var result = DateTimeHelpers.GetDateFromTimeStamp(timeStamp);
            Assert.AreEqual(date, result);
        }

        [Test]
        public void Test_WhereWasI_EqualsTrue()
        {
            var path = @"LocationHistoryVeryShort.json";
            var locationHistoryData = DataManager.GetLocationHistoryStruc(path);

            var result = LocationTracking.WhereWasI(locationHistoryData, "1548894299192");
            Assert.AreEqual(result.Latitude, 436872867);
            Assert.AreEqual(result.Longitude, -703617207);
        }

        [Test]
        public void Test_WhereWasI_ReturnsNull()
        {
            var path = @"LocationHistoryVeryShort.json";
            var locationHistoryData = DataManager.GetLocationHistoryStruc(path);

            var result = LocationTracking.WhereWasI(locationHistoryData, "88888888888888");
            Assert.IsNull(result);
        }

        [Test]
        public void Test_TimesCollide()
        {
            var result = DateTimeHelpers.TimesCollideMinute("1557116640", "1557116640");
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_TimesDontCollide()
        {
            var result = DateTimeHelpers.TimesCollideMinute("1557116640", "1570472454505");
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_LocationsCoincide_ExactLocation()
        {
            Location loc1 = new Location();
            Location loc2 = new Location();
            loc1.Latitude = 436872867;
            loc1.Longitude = -703617207;
            loc2.Latitude = 436872867;
            loc2.Longitude = -703617207;
            loc1.TimeStamp = "1557116640";
            loc2.TimeStamp = "1557116640";

            var result = LocationTracking.LocationsCollide(loc1, loc2, 2.0);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_LocationsCoincide_CloseByLocation()
        {
            Location loc1 = new Location();
            Location loc2 = new Location();
            loc1.Latitude = 436872867;
            loc1.Longitude = -703617207;
            loc2.Latitude = 436872848;
            loc2.Longitude = -703618346;
            loc1.TimeStamp = "1557116640";
            loc2.TimeStamp = "1557116640";

            var result = LocationTracking.LocationsCollide(loc1, loc2, 10.0);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_LocationsCoincide_TooFar()
        {
            Location loc1 = new Location();
            Location loc2 = new Location();
            loc1.Latitude = 436872867;
            loc1.Longitude = -703617207;
            loc2.Latitude = 436872848;
            loc2.Longitude = -703618346;
            loc1.TimeStamp = "1557116640";
            loc2.TimeStamp = "1557116640";

            var result = LocationTracking.LocationsCollide(loc1, loc2, 5.0);
            Assert.IsFalse(result);
        }

    }
}