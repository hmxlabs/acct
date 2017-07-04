using HmxLabs.Acct.Core.Config;
using HmxLabs.Acct.Core.Delivery;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.ReportGen;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Core.Log;

namespace HmxLabs.Acct.Core.Applet
{
    public class InvoiceSender
    {
        public const string DefaultConfigFile = "acct.config";

        public InvoiceSender(ILogger logger_ = null, string configFile_ = null)
        {
            Logger = logger_;
            ConfigFile = configFile_;
        }

        public ILogger Logger { get; set; }
        public string ConfigFile { get; set; }

        public void Run(ulong invoiceNumber_)
        {
            SetDefaultValuesIfNeeded();

            Logger.Info($"Reading invoice number {invoiceNumber_} from database...");
            var acctConfig = new PosixAcctConfig(ConfigFile);
            IInvoice invoice;
            using (var persister = new OleDbPersist(acctConfig.ConnectionString))
            {
                IInvoicePersist invoicePersist = persister;
                invoice = invoicePersist.Load(invoiceNumber_);
            }
            

            Logger.Info("Generating new invoice...");
            IInvoiceGen invoiceGen = new HtmlInvoiceGen(acctConfig.InvoiceTemplateFile, acctConfig.InvoiceItemTemplateFile);
            invoiceGen.OutputPath = acctConfig.InvoiceSaveLocation;
            var invoiceFile = invoiceGen.GenerateToDisk(invoice);
            Logger.Info($"New invoice written to location: {invoiceFile}");

            Logger.Info("Sending email with invoice...");
            var emailSenderConfig = new EmailInvoiceSenderConfig(acctConfig.All);
            IInvoiceSender invoiceSender = new EmailInvoiceSender(emailSenderConfig);
            invoiceSender.Send(invoice, invoiceFile);
            Logger.Info("Complete!");
        }

        private void SetDefaultValuesIfNeeded()
        {
            if (null == Logger)
            {
                Logger = new ConsoleLogger("Default");
                Logger.Notice("No logger was provided. Using a default console logger");
            }

            if (null == ConfigFile)
            {
                ConfigFile = DefaultConfigFile;
                Logger.Info("No configuration file was provided. Assuming a default file of acct.config in the working directory");
            }
        }
    }
}
