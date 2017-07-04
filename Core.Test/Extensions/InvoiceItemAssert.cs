using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public class InvoiceItemAssert
    {
        public static void AreEqual(IInvoiceItem reference_, IInvoiceItem underTest_)
        {
            Assert.That(underTest_.Id, Is.EqualTo(reference_.Id));
            Assert.That(underTest_.Description, Is.EqualTo(reference_.Description));
            Assert.That(underTest_.UnitCost, Is.EqualTo(reference_.UnitCost));
            Assert.That(underTest_.Quantity, Is.EqualTo(reference_.Quantity));
            Assert.That(underTest_.NetTotal, Is.EqualTo(reference_.NetTotal));
            Assert.That(underTest_.VatRate, Is.EqualTo(reference_.VatRate));
            Assert.That(underTest_.VatAmount, Is.EqualTo(reference_.VatAmount));
            Assert.That(underTest_.GrossTotal, Is.EqualTo(reference_.GrossTotal));

        }
    }
}
