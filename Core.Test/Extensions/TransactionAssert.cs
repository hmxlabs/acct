using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public class TransactionAssert
    {
        public static void AreEqual(ITransaction expected_, ITransaction underTest_)
        {
            Assert.That(underTest_.TransactionId, Is.EqualTo(expected_.TransactionId));
            AccountAssert.AreEqual(expected_.Account, underTest_.Account);
            Assert.That(underTest_.TransactionDate, Is.EqualTo(expected_.TransactionDate));
            Assert.That(underTest_.PostDate, Is.EqualTo(expected_.PostDate));
            Assert.That(underTest_.Description, Is.EqualTo(expected_.Description));
            Assert.That(underTest_.Amount, Is.EqualTo(expected_.Amount));
            Assert.That(underTest_.RunningBalance, Is.EqualTo(expected_.RunningBalance));
            Assert.That(underTest_.Notes, Is.EqualTo(expected_.Notes));

        }
    }
}
