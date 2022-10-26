using System;
using System.Collections.Generic;

namespace HmxLabs.Acct.Core.Models
{
    public interface IInvoice
    {
        ulong Number { get; }
        DateTime InvoiceDate { get; }
        IEntity Client { get; }
        decimal NetTotal { get; }
        decimal VatTotal { get; }
        decimal GrossTotal { get; }
        string Project { get; }
        string PurchaseOrder { get; }
        DateTime? PaymentDate { get; }
        InvoiceStatus Status { get; }
        IList<IInvoiceItem> Items { get; }

        void Validate();
        void Calculate();
    }
}
