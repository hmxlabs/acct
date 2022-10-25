using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.ReportGen.HtmlGen
{
    public interface IHtmlInvoiceGen : IInvoiceGen
    {
        string Generate(IInvoice invoice_);
    }
}
