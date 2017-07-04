using System.IO;

namespace HmxLabs.Acct.Core.Test.Data.Html
{
    public static class HtmlFileLocations
    {
        public static readonly string HtmlDirectoryRelativePath = Path.Combine(".", "Data", "Html");
        public static readonly string HtmlDirectoryAbsolutePath = Path.GetFullPath(HtmlDirectoryRelativePath);

        public static readonly string HtmlTestOutputRelativePath = Path.Combine(".", "TestOutput", "Html");
        public static readonly string HtmlTestOutputAbsolutePath = Path.GetFullPath(HtmlTestOutputRelativePath);

        public const string InvoiceTemplateFilname = "Invoice.Template.html";
        public static readonly string InvoiceTemplate = Path.Combine(HtmlDirectoryAbsolutePath, InvoiceTemplateFilname);

        public const string InvoiceItemTemplateFilename = "InvoiceIItem.Template.html";
        public static readonly string InvoiceItemTemplate = Path.Combine(HtmlDirectoryAbsolutePath, InvoiceItemTemplateFilename);

        public static string UnsentSampleInvoiceFilename = "Sample.Unsent.Invoice.html";
        public static readonly string UnsentSampleInvoice = Path.Combine(HtmlDirectoryAbsolutePath, UnsentSampleInvoiceFilename);

        public static string SentSampleInvoiceFilename = "Sample.Sent.Invoice.html";
        public static readonly string SentSampleInvoice = Path.Combine(HtmlDirectoryAbsolutePath, SentSampleInvoiceFilename);
    }
}
