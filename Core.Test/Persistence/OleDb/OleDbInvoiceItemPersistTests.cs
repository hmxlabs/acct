using System.Linq;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbInvoiceItemPersistTests
    {
        [Test]
        public void TestLoadItemsForInvoiceId()
        {
            IInvoiceItemPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var items = persister.Load(Data.InvoiceItem.Widgets.Values.InvoiceId).ToList();
            Assert.That(items.Count, Is.EqualTo(2));
            IInvoiceItem item = items.First();
            InvoiceItemAssert.AreEqual(Data.InvoiceItem.Widgets.Instance, item);
        }
    }
}
