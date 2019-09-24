using NUnit.Framework;
using DollarAddresses;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            DollarAddress da = new DollarAddress();

            Assert.AreEqual(71, da.CalculateValue("High St"));
            Assert.AreNotEqual(71, da.CalculateValue("Higher St"));

        }
        [Test]
        public void Test2()
        {
            DollarAddress da = new DollarAddress();
            DollarAddress.Address_records.Address obj1 = new DollarAddress.Address_records.Address();
            DollarAddress.Address_records.Address obj2 = new DollarAddress.Address_records.Address();

            obj1.ADDRESS_NUMBER = 71;
            obj1.STREETNAME = "High";
            obj1.SUFFIX = "St";

            obj2.ADDRESS_NUMBER = 71;
            obj2.STREETNAME = "Tom";
            obj2.SUFFIX = "St";

            Assert.IsTrue(da.IsItDollarAddress(obj1));
            Assert.IsFalse(da.IsItDollarAddress(obj2));

        }
    }
}