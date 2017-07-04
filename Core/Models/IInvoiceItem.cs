using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public interface IInvoiceItem
    {
        IInvoice ParentInvoice { get; }
        ulong Id { get; }
        string Description { get; }
        decimal UnitCost { get; }
        decimal Quantity { get; }
        decimal NetTotal { get; }
        decimal VatRate { get; }
        decimal VatAmount { get; }
        decimal GrossTotal { get; }

        void Validate();

        void Calculate();
    }
}
