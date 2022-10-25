using System;
using System.Linq;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbTransactionPersistTests
    {
        [Test]
        public void TestArgumentGuards()
        {
            ITransactionPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            Assert.Throws<ArgumentNullException>(() => persister.SaveNew(null));
            Assert.Throws<ArgumentNullException>(() => persister.Update(null));
        }

        [Test]
        public void TestLoadInPeriod()
        {
            ITransactionPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var startAndEndDate = new DateTime(2001, 01, 01);
            var transactions = persister.LoadInPeriod(startAndEndDate, startAndEndDate).ToList();

            Assert.That(transactions.Count, Is.EqualTo(1));
            var transaction = transactions.First();
            TransactionAssert.AreEqual(Transaction.Sample.Instance, transaction);
        }

        [Test]
        public void TestSaveNewAndLoad()
        {
            ITransactionPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var startAndEndDate = new DateTime(1900, 01, 01);
            var transactions = persister.LoadInPeriod(startAndEndDate, startAndEndDate).ToList();
            Assert.That(transactions.Count, Is.EqualTo(0), "The database already contains the records that would be used for the save test. Re-run the build to clear the database");

            var transaction = new Core.Models.Transaction();
            transaction.Account = Account.CurrentAccount.Instant;
            transaction.TransactionDate = startAndEndDate;
            transaction.PostDate = startAndEndDate;
            transaction.Description = "New transaction description";
            transaction.Amount = 123.45M;
            transaction.RunningBalance = 987.65M;
            transaction.Notes = "new transaction notes. created by unit tests";

            persister.SaveNew(transaction);

            transactions = persister.LoadInPeriod(startAndEndDate, startAndEndDate).ToList();
            Assert.That(transactions.Count, Is.EqualTo(1));
            var retrievedTransaction = transactions.First();
            transaction.TransactionId = retrievedTransaction.TransactionId; // This should be next transaction number allocated. What was set before is not relevant.
            TransactionAssert.AreEqual(transaction, retrievedTransaction);
        }
    }
}
