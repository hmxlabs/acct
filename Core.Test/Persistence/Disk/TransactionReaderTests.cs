using System;
using System.IO;
using HmxLabs.Acct.Core.Persistence.Disk;
using HmxLabs.Acct.Core.Test.Data.Transactions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.Disk
{
    [TestFixture]
    public abstract class TransactionReaderTests
    {
        [Test]
        public void TestArgumentGuards()
        {
            var reader = GetReader();
            Assert.Throws<ArgumentNullException>(() => reader.Read(null));
            Assert.Throws<ArgumentException>(() => reader.Read("  "));
            Assert.Throws<FileNotFoundException>(() => reader.Read("some.random.file.csv"));
        }

        [Test]
        public void TestReadThrowsWithNullTransactionAccount()
        {
            var reader = GetReader();
            reader.TransactionAccount = null;
            Assert.Throws<InvalidOperationException>(() => reader.Read(TransactionFileLocations.CaterAllenSample));
        }

        protected abstract ITransactionFileReader GetReader();
    }
}
