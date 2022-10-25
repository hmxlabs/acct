using System;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public class EmailHtmlBodyGenConfig : IHtmlInvoiceGenConfig
    {
        public static class EmailHtmlBodyGenConfigKeys
        {
            public const string TemplateFile = "acct.email.html.template";
            public const string ItemTemplateFile = "acct.email.html.itemtemplate";
        }

        public EmailHtmlBodyGenConfig(string templateFile_, string itemTemplateFile_)
        {
            if (null == templateFile_)
                throw new ArgumentNullException(nameof(templateFile_));

            if (null == itemTemplateFile_)
                throw new ArgumentNullException(nameof(itemTemplateFile_));

            if (string.IsNullOrWhiteSpace(templateFile_))
                throw new ArgumentException("Empty or whitespace template filename provided", nameof(templateFile_));

            if (string.IsNullOrWhiteSpace(itemTemplateFile_))
                throw new ArgumentException("Empty or whitespace item template filename provided", nameof(itemTemplateFile_));

            InvoiceTemplateFile = templateFile_;
            InvoiceItemTemplateFile = itemTemplateFile_;
        }

        public EmailHtmlBodyGenConfig(IConfigProvider config_)
        {
            if (null == config_)
                throw new ArgumentNullException(nameof(config_));

            InvoiceTemplateFile = config_.GetConifgAsStringStrict(EmailHtmlBodyGenConfigKeys.TemplateFile);
            InvoiceItemTemplateFile = config_.GetConifgAsStringStrict(EmailHtmlBodyGenConfigKeys.ItemTemplateFile);
        }

        public string SaveLocation => null;
        public string InvoiceTemplateFile { get; }
        public string InvoiceItemTemplateFile { get; }
    }
}
