using System.Diagnostics;
using HmxLabs.Acct.Core.Config;
using HmxLabs.Acct.Core.Delivery;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.ReportGen;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Acct.Core.ReportGen.PdfGen;
using HmxLabs.Core.Log;

namespace HmxLabs.Acct.Core.Applet
{
    public class InvoiceSender : BaseApp
    {
        public InvoiceSender(ILogger logger_ = null, string configFile_ = null) : base(logger_, configFile_)
        {
        }
        
        public void Run(ulong invoiceNumber_)
        {
            Initialise();

            Logger.Info($"Using configuration from {Config.ConfigFileRead}");
            Logger.Info($"Reading invoice number {invoiceNumber_} from database...");
            IInvoice invoice;
            using (var persister = new OleDbPersist(Config.ConnectionString))
            {
                IInvoicePersist invoicePersist = persister;
                invoice = invoicePersist.Load(invoiceNumber_);
            }
            

            Logger.Info("Generating new invoice...");
            IHtmlInvoiceGenConfig htmlInvoiceGenConfig = new HtmlInvoiceGenConfig(Config.All);
            IHtmlInvoiceGen htmlInvoiceGen = new HtmlInvoiceGen(htmlInvoiceGenConfig);
            IInvoiceGen pdfInvoiceGen = new PdfFromHtmlGenerator(htmlInvoiceGen, Config.InvoicePdfSaveLocation);

            var invoiceFile = pdfInvoiceGen.GenerateToDisk(invoice);
            Logger.Info($"New invoice written to location: {invoiceFile}");
            Logger.Info($"Showing invoice: {invoiceFile}");
            Process.Start(invoiceFile);

            Logger.Info("Sending email with invoice...");
            var emailSenderConfig = new EmailInvoiceSenderConfig(Config.All);
            var emailHtmlBodyGenConfig = new EmailHtmlBodyGenConfig(Config.All);
            IHtmlInvoiceGen emailHtmlBodyGen = new HtmlInvoiceGen(emailHtmlBodyGenConfig);
            IMailConstructor mailConstructor = new MailConstructor(emailSenderConfig, emailHtmlBodyGen);
            IInvoiceSender invoiceSender = new EmailInvoiceSender(emailSenderConfig, mailConstructor);
            invoiceSender.Send(invoice, invoiceFile);
            Logger.Info("Complete!");
        }

        
    }
}
