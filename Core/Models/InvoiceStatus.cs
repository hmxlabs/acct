using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public enum InvoiceStatus
    {
        New,
        Sent,
        Paid
    }

    public static class InvoiceStatusExtensions
    {
        public static InvoiceStatus Parse(string status_)
        {
            return (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), status_);
        }
    }
}
