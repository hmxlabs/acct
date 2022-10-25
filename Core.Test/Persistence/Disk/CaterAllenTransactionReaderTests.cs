using System.Linq;
using HmxLabs.Acct.Core.Persistence.Disk;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Data.Transactions;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.Disk
{
    [TestFixture]
    public class CaterAllenTransactionReaderTests : TransactionReaderTests
    {
        [Test]
        public void ReadTestFileWithSingleTransaction()
        {
            var reader = GetReader();
            var transactions = reader.Read(TransactionFileLocations.CaterAllenSample).ToList();

            Assert.That(transactions.Count, Is.EqualTo(1));
            var transaction = transactions.First();
            TransactionAssert.AreEqual(Transaction.CaterAllen.Instance, transaction);
        }

        [Test]
        public void ReadTestFileWithMultipleTransactions()
        {
            var reader = GetReader();
            var transactions = reader.Read(TransactionFileLocations.CaterAllenMultipleSample).ToList();

            Assert.That(transactions.Count, Is.EqualTo(4));

            var currTransDate = transactions.First().TransactionDate;
            foreach (var transaction in transactions)
            {
                // Check that the file has been reversed and we have the oldest transaction first (and that its ordered but it should be already...)
                Assert.That(currTransDate, Is.LessThanOrEqualTo(transaction.TransactionDate));
            }
        }

        protected override ITransactionFileReader GetReader()
        {
            var reader = new CaterAllenTransactionReader();
            reader.TransactionAccount = Account.CurrentAccount.Instant;
            return reader;
        }
    }
}
