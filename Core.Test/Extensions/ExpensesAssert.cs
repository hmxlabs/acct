using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public class ExpensesAssert
    {
        public static void AreEqual(IExpense reference_, IExpense underTest_)
        {
            if (null == reference_ && null == underTest_)
                return;

            if (null == reference_)
                Assert.Fail("Expected a null instance of an IExpense but a non null instance was provided");

            if (null == underTest_)
                Assert.Fail("Expected a non null instance of an IExpense but a null instance was provided");

            Assert.That(underTest_.ExpenseId, Is.EqualTo(reference_.ExpenseId));
            Assert.That(underTest_.InvoiceDate, Is.EqualTo(reference_.InvoiceDate));
            Assert.That(underTest_.PaymentDate, Is.EqualTo(reference_.PaymentDate));
            Assert.That(underTest_.InvoiceNumber, Is.EqualTo(reference_.InvoiceNumber));
            EntityAssert.AreEqual(reference_.Supplier, underTest_.Supplier);
            Assert.That(underTest_.Description, Is.EqualTo(reference_.Description));
            Assert.That(underTest_.AmountPreVat, Is.EqualTo(reference_.AmountPreVat));
            Assert.That(underTest_.Vat, Is.EqualTo(reference_.Vat));
            Assert.That(underTest_.TotalInclVat, Is.EqualTo(reference_.TotalInclVat));
            Assert.That(underTest_.Project, Is.EqualTo(reference_.Project));
            Assert.That(underTest_.PaymentMethod, Is.EqualTo(reference_.PaymentMethod));

        }
    }
}
