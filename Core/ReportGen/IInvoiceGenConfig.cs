using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.ReportGen
{
    public interface IInvoiceGenConfig
    {
        string SaveLocation { get; }
    }
}
