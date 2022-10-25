using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.ReportGen.HtmlGen
{
    public interface IHtmlInvoiceGenConfig : IInvoiceGenConfig
    {
        string InvoiceTemplateFile { get; }
        string InvoiceItemTemplateFile { get; }
    }
}
