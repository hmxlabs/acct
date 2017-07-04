using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbInvoicePersistTest
    {
        [Test]
        public void TeatLoadSingleInvoice()
        {
            IInvoicePersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var invoice = persister.Load(Invoice.Unsent.Values.Number);
            InvoiceAssert.AreEqual(Invoice.Unsent.Instance, invoice);
        }

        [Test]
        public void TestLoadInvoiceWithoutPaymentDate()
        {
            IInvoicePersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var invoice = persister.Load(Invoice.Sent.Values.Number);
            InvoiceAssert.AreEqual(Invoice.Sent.Instance, invoice);
        }
    }
}
