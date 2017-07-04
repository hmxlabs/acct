using System.Linq;
using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public class InvoiceAssert
    {
        public static void AreEqual(IInvoice reference_, IInvoice underTest_)
        {
            if (null == reference_ && null == underTest_)
                return;

            if (null == reference_)
                Assert.Fail("Expected a null instance of an IInvoice but a non null instance was provided");

            if (null == underTest_)
                Assert.Fail("Expected a non null instance of an IInvoice but a null instance was provided");

            Assert.That(underTest_.Number, Is.EqualTo(reference_.Number));
            Assert.That(underTest_.InvoiceDate, Is.EqualTo(reference_.InvoiceDate));
            EntityAssert.AreEqual(reference_.Client, underTest_.Client);
            Assert.That(underTest_.NetTotal, Is.EqualTo(reference_.NetTotal));
            Assert.That(underTest_.VatTotal, Is.EqualTo(reference_.VatTotal));
            Assert.That(underTest_.GrossTotal, Is.EqualTo(reference_.GrossTotal));
            Assert.That(underTest_.Project, Is.EqualTo(reference_.Project));
            Assert.That(underTest_.PaymentDate, Is.EqualTo(reference_.PaymentDate));
            Assert.That(underTest_.Status, Is.EqualTo(reference_.Status));

            var refItemList = reference_.Items.ToList();
            var testItemList = underTest_.Items.ToList();

            Assert.That(testItemList.Count, Is.EqualTo(refItemList.Count));
            for (var itemIndex = 0; itemIndex < refItemList.Count; itemIndex++)
            {
                InvoiceItemAssert.AreEqual(refItemList[itemIndex], testItemList[itemIndex]);
            }

            EntityAssert.AreEqual(reference_.Client, underTest_.Client);
        }
    }
}
