using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public static class AddressAssert
    {
        public static void AreEqual(IAddress reference_, IAddress underTest_)
        {
            Assert.AreEqual(reference_.DoorNumber, underTest_.DoorNumber);
            Assert.AreEqual(reference_.Building, underTest_.Building);
            Assert.AreEqual(reference_.StreetNumber, underTest_.StreetNumber);
            Assert.AreEqual(reference_.StreetName, reference_.StreetName);
            Assert.AreEqual(reference_.City, underTest_.City);
            Assert.AreEqual(reference_.PostCode, underTest_.PostCode);
            Assert.AreEqual(reference_.Country, underTest_.Country);
        }
    }
}
