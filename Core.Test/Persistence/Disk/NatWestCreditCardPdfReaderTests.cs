using System;
using System.Linq;
using HmxLabs.Acct.Core.Persistence.Disk;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Data.Transactions;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.Disk
{
    [TestFixture]
    public class NatWestCreditCardPdfReaderTests : TransactionReaderTests
    {
        [Test]
        public void ReadTestFileWithSingleTransaction()
        {
            var reader = GetReader();
            var transactions = reader.Read(TransactionFileLocations.NatWestCreditCardPdfSample).ToList();

            Assert.That(transactions.Count, Is.EqualTo(8));
            var transaction = transactions.First();
            TransactionAssert.AreEqual(Transaction.NatWestPdf.Instance, transaction);

            transaction = transactions[1];
            Assert.That(transaction.Amount, Is.EqualTo(-13.19));
            Assert.That(transaction.Description, Is.EqualTo("ACME WIDGETS INC - REF: 47845216"));
            Assert.That(transaction.PostDate.Year, Is.EqualTo(2021));

            transaction = transactions.Last();
            Assert.That(transaction.Amount, Is.EqualTo(211.63M));
            Assert.That(transaction.Description, Is.EqualTo("FASTER PAYMENT RECEIVED - THANK YOU - REF: 00268600"));
            Assert.That(transaction.RunningBalance, Is.EqualTo(-58.69M));
        }

        [Test]
        public void TestCheckSumFailureThrowsException()
        {
            var reader = GetReader();
            Assert.Throws<Exception>(() => reader.Read(TransactionFileLocations.NatWestCreditCardPdfFailChecksum));
        }

        [Test]
        public void TestYearParseFailureThrowsException()
        {
            var reader = GetReader();
            Assert.Throws<Exception>(() => reader.Read(TransactionFileLocations.NatWestCreditCardPdfFailYearParse));
        }

        protected override ITransactionFileReader GetReader()
        {
            var reader = new NatWestCreditCardPdfReader();
            reader.TransactionAccount = Account.CurrentAccount.Instant;
            return reader;
        }
    }
}
