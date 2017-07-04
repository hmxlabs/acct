using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public static class EntityAssert
    {
        public static void AreEqual(IEntity reference_, IEntity underTest_)
        {
            Assert.That(underTest_.Id, Is.EqualTo(reference_.Id));
            Assert.AreEqual(reference_.Id, underTest_.Id);
            Assert.AreEqual(reference_.Name, underTest_.Name);
            Assert.AreEqual(reference_.FirstName, underTest_.FirstName);
            Assert.AreEqual(reference_.Website, underTest_.Website);
            Assert.AreEqual(reference_.Phone, underTest_.Phone);
            Assert.AreEqual(reference_.IsCorp, underTest_.IsCorp);
            Assert.AreEqual(reference_.BillingEmail, reference_.BillingEmail);
            AddressAssert.AreEqual(reference_.Address, underTest_.Address);
        }
    }
}
