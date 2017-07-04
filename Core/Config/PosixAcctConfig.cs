using System;
using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Config
{
    public class PosixAcctConfig : IAcctConfig
    {
        public static class ConfigKeys
        {
            public const string ConnectionString = "acct.connectionstring";
            public const string InvoiceSaveLocation = "acct.invoice.savelocation";
            public const string InvoiceTemplateFile = "acct.invoice.template";
            public const string InvoiceItemTemplateFile = "acct.invoice.itemtemplate";
        }

        public PosixAcctConfig(string filename_)
        {
            if (null == filename_)
                throw new ArgumentNullException(nameof(filename_));

            if (string.IsNullOrWhiteSpace(filename_))
                throw new ArgumentException("An empty or whitespace filename was specified for the configuration file", nameof(filename_));

            var posixReader = new PosixConfigReader(filename_);
            ConnectionString = posixReader.GetConifgAsStringStrict(ConfigKeys.ConnectionString);
            InvoiceSaveLocation = posixReader.GetConifgAsStringStrict(ConfigKeys.InvoiceSaveLocation);
            InvoiceTemplateFile = posixReader.GetConifgAsStringStrict(ConfigKeys.InvoiceTemplateFile);
            InvoiceItemTemplateFile = posixReader.GetConifgAsStringStrict(ConfigKeys.InvoiceItemTemplateFile);
            All = posixReader;
        }

        public string ConnectionString { get; }
        public string InvoiceSaveLocation { get; }
        public string InvoiceTemplateFile { get; }
        public string InvoiceItemTemplateFile { get; }
        public IConfigProvider All { get; }
    }
}
