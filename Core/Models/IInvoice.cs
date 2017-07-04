using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        DateTime? PaymentDate { get; }
        InvoiceStatus Status { get; }
        IList<IInvoiceItem> Items { get; }

        void Validate();
        void Calculate();
    }
}
