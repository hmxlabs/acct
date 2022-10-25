using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Core.DateTIme;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public class MailConstructor : IMailConstructor
    {
        public MailConstructor(IEmailInvoiceSenderConfig config_, IHtmlInvoiceGen htmlEMailBodyGen_ = null)
        {
            if (null == config_)
                throw new ArgumentNullException(nameof(config_));

            Config = config_;
            HtmlEMailBodyGen = htmlEMailBodyGen_;
        }

        public MailMessage Create(IInvoice invoice_, string generatedInvoice_)
        {
            if (null == invoice_)
                throw new ArgumentNullException(nameof(invoice_));

            if (null == generatedInvoice_)
                throw new ArgumentNullException(nameof(generatedInvoice_));

            if (string.IsNullOrWhiteSpace(generatedInvoice_))
                throw new ArgumentException("No filename specified for the generated invoice", nameof(generatedInvoice_));

            if (!File.Exists(generatedInvoice_))
                throw new FileNotFoundException($"The specified generated invoice file does not exist. File not found: {generatedInvoice_}");

            var recipient = new MailAddress(invoice_.Client.BillingEmail, invoice_.Client.DisplayName);
            var message = new MailMessage(Config.Sender, recipient);
            message.CC.Add(Config.Sender);
            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = CreateSubject(invoice_);
            message.BodyEncoding = Encoding.UTF7;
            message.Body = CreateBody(invoice_);
            message.IsBodyHtml = false;
            message.Attachments.Add(CreateAttachment(invoice_, generatedInvoice_));

            if (null == HtmlEMailBodyGen)
                return message;

            var html = HtmlEMailBodyGen.Generate(invoice_);
            var altView = AlternateView.CreateAlternateViewFromString(html, new ContentType("text/html"));
            message.AlternateViews.Add(altView);
            return message;
        }
        
        private string CreateBody(IInvoice invoice_)
        {
            var body = new StringBuilder();
            body.AppendLine(Config.Greeting);
            body.AppendLine(string.Empty);

            body.AppendLine("Please find attached your invoice:");
            body.AppendLine(string.Empty);
            body.Append("Invoice Number: ");
            body.AppendLine(invoice_.Number.ToString());
            body.Append("Date: ");
            body.AppendLine(invoice_.InvoiceDate.ToExplicitDateDisplayString());
            body.Append("Total Payable: £");
            body.AppendLine(invoice_.GrossTotal.ToCurrencyString());
            body.AppendLine(string.Empty);
            body.Append("for the following items:");

            foreach (var invoiceItem in invoice_.Items)
            {
                body.Append("- ");
                body.AppendLine(invoiceItem.Description);
            }

            body.AppendLine(string.Empty);
            body.AppendLine(Config.SignOff);
            body.AppendLine(string.Empty);

            return body.ToString();
        }

        private Attachment CreateAttachment(IInvoice invoice_, string filename_)
        {
            var attachment = new Attachment(filename_);
            var extension = Path.GetExtension(filename_);
            attachment.ContentDisposition.FileName = $"{Config.AttachmentBaseName}-N{invoice_.Number}-D{invoice_.InvoiceDate.ToExplicitDateDisplayString()}.{extension}";

            return attachment;
        }

        private string CreateSubject(IInvoice invoice_)
        {
            return $"{Config.SubjectBase} - Number {invoice_.Number} - Date {invoice_.InvoiceDate.ToExplicitDateDisplayString()}";
        }

        public IEmailInvoiceSenderConfig Config { get; }

        public IHtmlInvoiceGen HtmlEMailBodyGen { get; }
    }
}
