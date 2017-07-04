using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface IInvoicePersist
    {
        IInvoice Load(ulong id_);
        void Save(IInvoice invoice_);
        void MarkAsSent(ulong id_);
    }
}
