using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.ReportGen
{
    public interface IInvoiceGen
    {
        string OutputPath { set; get; }

        string GenerateToDisk(IInvoice invoice_);

        string Generate(IInvoice invoice_);
    }
}
