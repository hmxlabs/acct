using System;
using System.Linq;
using HmxLabs.Acct.Core.Applet;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.Disk;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Data.Transactions;
using HmxLabs.Acct.Core.Test.Persistence.OleDb;
using HmxLabs.Core.Log;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Applet
{
    [TestFixture]
    public class TransactionImporterTests
    {
        public static readonly DateTime StartDate = new DateTime(2021, 1, 1);
        public static readonly DateTime EndDate = new DateTime(2021, 1, 3);

        [Test]
        public void ImportTransactionsWithDuplicates()
        {
            var applet = new TransactionImporter(TraceLogger.Instance, ConfigFileLocations.AppletConfig);
            applet.Run("Current", TransactionFileLocations.ImportSample, TransactionFileType.CaterAllen);

            var dbReader = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var transactionReader = (ITransactionPersist) dbReader;

            var transactions = transactionReader.LoadInPeriod(StartDate, EndDate).ToList();

            Assert.That(transactions.Count, Is.EqualTo(6)); // 5 in the sample file plus 1 existing one that is duplicated
            Assert.That(transactions.Last().Notes, Contains.Substring("**SUSPECTED DUPLICATE TRANSACTION - PLEASE VALIDATE**"));
            Assert.That(transactions.First().Notes, Is.Null);
        }
    }
}
