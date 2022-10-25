using System;
using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.ReportGen.HtmlGen
{
    public class HtmlInvoiceGenConfig : IHtmlInvoiceGenConfig
    {
        public static class HtmlInvoiceGenConfigKeys
        {
            public const string SaveLocation = "acct.invoice.html.savelocation";
            public const string TemplateFile = "acct.invoice.html.template";
            public const string ItemTemplateFile = "acct.invoice.html.itemtemplate";
        }

        public HtmlInvoiceGenConfig(string saveLocation_, string invoiceTemplateFile_, string invoiceItemTemplateFile_)
        {
            if (null == saveLocation_)
                throw new ArgumentNullException(nameof(saveLocation_));

            if (null == invoiceItemTemplateFile_)
                throw new ArgumentNullException(nameof(invoiceItemTemplateFile_));

            if (null == invoiceTemplateFile_)
                throw new ArgumentNullException(nameof(invoiceTemplateFile_));

            SaveLocation = saveLocation_;
            InvoiceTemplateFile = invoiceTemplateFile_;
            InvoiceItemTemplateFile = invoiceItemTemplateFile_;
        }

        public HtmlInvoiceGenConfig(IConfigProvider configProvider_)
        {
            if (null == configProvider_)
                throw new ArgumentNullException(nameof(configProvider_));

            SaveLocation = configProvider_.GetConifgAsStringStrict(HtmlInvoiceGenConfigKeys.SaveLocation);
            InvoiceTemplateFile = configProvider_.GetConifgAsStringStrict(HtmlInvoiceGenConfigKeys.TemplateFile);
            InvoiceItemTemplateFile = configProvider_.GetConifgAsStringStrict(HtmlInvoiceGenConfigKeys.ItemTemplateFile);
        }

        public string SaveLocation { get; }
        public string InvoiceTemplateFile { get; }
        public string InvoiceItemTemplateFile { get; }
    }
}
