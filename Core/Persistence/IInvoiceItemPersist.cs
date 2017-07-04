using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface IInvoiceItemPersist
    {
        IEnumerable<IInvoiceItem> Load(ulong invoiceId_);
        void Save(IInvoiceItem invoiceItem_);
    }
}
