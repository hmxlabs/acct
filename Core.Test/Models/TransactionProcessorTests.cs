using System;
using System.Collections.Generic;
using System.Linq;
using HmxLabs.Acct.Core.Cache;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Models
{
    [TestFixture]
    public class TransactionProcessorTests
    {
        [Test]
        public void TestArgumentGuards()
        {
            var processor = new TransactionProcessor();
            Assert.Throws<ArgumentNullException>(() => processor.FindMostRecentTransaction(null));
            Assert.Throws<ArgumentNullException>(() => processor.FindOldestTransaction(null));

            Assert.Throws<ArgumentException>(() => processor.FindOldestTransaction(new ITransaction[0]));
            Assert.Throws<ArgumentException>(() => processor.FindMostRecentTransaction(new ITransaction[0]));
        }

        [Test]
        public void TestFindOldestTransaction()
        {
            var processor = new TransactionProcessor();
            var oldestTransaction = processor.FindOldestTransaction(CreateTransactions());
            Assert.That(oldestTransaction.TransactionDate, Is.EqualTo(OldestTransactionDate));

        }

        [Test]
        public void TestFindMostRecentTransaction()
        {
            var processor = new TransactionProcessor();
            var oldestTransaction = processor.FindMostRecentTransaction(CreateTransactions());
            Assert.That(oldestTransaction.TransactionDate, Is.EqualTo(MostRecentTransactionDate));
        }

        [Test]
        public void TestMarkDuplicateTransactions()
        {
            var newTransactions = new List<ITransaction>();
            var existingTransactoins = new TransactionCache();

            newTransactions.Add(Data.Transaction.CaterAllen.Instance);
            newTransactions.Add(Data.Transaction.Sample.Instance);
            existingTransactoins.Put(Data.Transaction.CaterAllen.Instance);

            var processor = new TransactionProcessor();
            var checkedTransactions = processor.MarkDupicateTransactions(newTransactions, existingTransactoins);

            Assert.That(checkedTransactions.DuplicatedTransactions.Count(), Is.EqualTo(1));
            Assert.That(checkedTransactions.UniqueTransactions.Count(), Is.EqualTo(1));

            TransactionAssert.AreEqual(Data.Transaction.CaterAllen.Instance, checkedTransactions.DuplicatedTransactions.First());
            TransactionAssert.AreEqual(Data.Transaction.Sample.Instance, checkedTransactions.UniqueTransactions.First());
        }

        private List<Transaction> CreateTransactions()
        {
            return TransactionDates.Select(transactionDate_ => new Transaction {TransactionDate = transactionDate_}).ToList();
        }

        private static readonly DateTime OldestTransactionDate = new DateTime(1999, 12, 31);
        private static readonly DateTime MostRecentTransactionDate = new DateTime(2001, 1, 1);
        private static readonly DateTime[] TransactionDates = {new DateTime(2000, 1, 1), new DateTime(2000, 1, 2), new DateTime(2000, 2, 1), new DateTime(2000, 3, 1), OldestTransactionDate, MostRecentTransactionDate };
    }
}
