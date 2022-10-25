using HmxLabs.Acct.Core.ReportGen.HtmlGen;

namespace HmxLabs.Acct.Core.Test.Data.Config
{
    public class TestEmailHtmlBodyGenConfig : IHtmlInvoiceGenConfig
    {
        public static class ConfigValues
        {
            public const string InvoiceHtmlSaveLocation = null;
            public const string InvoiceTemplateFile = "C:\\acct\\templates\\email-invoice.html";
            public const string InvoiceItemTemplateFile = "C:\\acct\\templates\\email-invoice-item.html";
        }

        public static TestEmailHtmlBodyGenConfig Instance => new TestEmailHtmlBodyGenConfig();

        public string SaveLocation => ConfigValues.InvoiceHtmlSaveLocation;
        public string InvoiceTemplateFile => ConfigValues.InvoiceTemplateFile;
        public string InvoiceItemTemplateFile => ConfigValues.InvoiceItemTemplateFile;
    }
}
