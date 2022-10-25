using HmxLabs.Acct.Core.ReportGen.HtmlGen;

namespace HmxLabs.Acct.Core.Test.Data.Config
{
    public class TestHtmlInvoiceGenConfig : IHtmlInvoiceGenConfig
    {
        public static class ConfigValues
        {
            public const string InvoiceHtmlSaveLocation = "C:\\acct\\invoices\\html";
            public const string InvoiceTemplateFile = "C:\\acct\\templates\\invoice.html";
            public const string InvoiceItemTemplateFile = "C:\\acct\\templates\\invoice-item.html";
        }

        public static TestHtmlInvoiceGenConfig Instance => new TestHtmlInvoiceGenConfig();

        public string SaveLocation => ConfigValues.InvoiceHtmlSaveLocation;
        public string InvoiceTemplateFile => ConfigValues.InvoiceTemplateFile;
        public string InvoiceItemTemplateFile => ConfigValues.InvoiceItemTemplateFile;
    }
}
