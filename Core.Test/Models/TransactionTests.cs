using System;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Models
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestKeyGeneration()
        {
            var trans = new Transaction();
            trans.Amount = 123.45M;
            trans.Description = "Test transaction DETAILS";
            trans.PostDate = new DateTime(2017, 1, 10);
            trans.TransactionDate = new DateTime(2017, 1, 11);

            const string expectedKey = "2017-01-11||2017-01-10||test transaction details||123.45";
            Assert.That(trans.Key, Is.EqualTo(expectedKey));
        }

        [Test]
        public void TestKeyGenerationWhenAmountIsWholeNumbers()
        {
            var trans = new Transaction();
            trans.Amount = 123.00M;
            trans.Description = "Test transaction DETAILS";
            trans.PostDate = new DateTime(2017, 1, 10);
            trans.TransactionDate = new DateTime(2017, 1, 11);

            var expectedKey = "2017-01-11||2017-01-10||test transaction details||123.00";
            Assert.That(trans.Key, Is.EqualTo(expectedKey));

            trans.Amount = 123.10M;
            expectedKey = "2017-01-11||2017-01-10||test transaction details||123.10";
            Assert.That(trans.Key, Is.EqualTo(expectedKey));
        }

        [Test]
        public void TestCopyConstructorArgumentGuard()
        {
            Assert.Throws<ArgumentNullException>(() => new Transaction(null));
        }

        [Test]
        public void TestCopyConstructor()
        {
            var newTransaction = new Transaction(Data.Transaction.CaterAllen.Instance);
            TransactionAssert.AreEqual(Data.Transaction.CaterAllen.Instance, newTransaction);
        }
    }
}
