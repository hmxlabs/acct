using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Delivery
{
    public interface IInvoiceSender
    {
        void Send(IInvoice invoice_, string generatedInvoiceLocation_);
    }
}
