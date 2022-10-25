using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Persistence.Disk;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.Disk
{
    [TestFixture]
    public class TransactionFileReaderFactoryTests
    {
        [Test]
        public void TestCaterAllenReaderIsCreated()
        {
            var factory = new TransactionFileReaderFactory();
            var reader = factory.Create(TransactionFileType.CaterAllen, null);
            Assert.That(reader, Is.TypeOf(typeof(CaterAllenTransactionReader)));
        }
    }
}
