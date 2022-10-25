using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public static class AccountAssert
    {
        public static void AreEqual(IAccount reference_, IAccount underTest_)
        {
            Assert.AreEqual(reference_.AccountId, underTest_.AccountId);
            Assert.AreEqual(reference_.AccountNumber, underTest_.AccountNumber);
            Assert.AreEqual(reference_.Currency, underTest_.Currency);
            Assert.AreEqual(reference_.Provider, underTest_.Provider);
            Assert.AreEqual(reference_.Type, reference_.Type);
            Assert.AreEqual(reference_.Description, underTest_.Description);
        }
    }
}
