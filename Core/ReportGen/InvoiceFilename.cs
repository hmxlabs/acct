using System;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Core.DateTIme;

namespace HmxLabs.Acct.Core.ReportGen
{
    public class InvoiceFilename
    {
        public static string Generate(IInvoice invoice_, string fileExtension_)
        {
            if (null == invoice_)
                throw new ArgumentNullException(nameof(invoice_));

            if (null == fileExtension_)
                throw new ArgumentNullException(nameof(fileExtension_));

            if (string.IsNullOrWhiteSpace(fileExtension_))
                throw new ArgumentException("Empty or whitespace file extension provided", nameof(fileExtension_));

            return $"{invoice_.InvoiceDate.ToIsoDateString()}--{invoice_.Number}--{invoice_.Client.DisplayName}.{fileExtension_}";
        }
    }
}
