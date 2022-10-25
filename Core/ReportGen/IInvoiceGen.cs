using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.ReportGen
{
    public interface IInvoiceGen
    {
        string GenerateToDisk(IInvoice invoice_);
    }
}
