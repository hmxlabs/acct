using System;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Core.Net.Mail;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public class EmailInvoiceSender : IEmailInvoiceSender
    {
        public EmailInvoiceSender(IEmailInvoiceSenderConfig config_, IMailConstructor constructor_ = null, IMailSender sender_ = null)
        {
            if (null == config_)
                throw new ArgumentNullException(nameof(config_));

            Config = config_;
            Constructor = constructor_ ?? new MailConstructor(config_);
            Sender = sender_ ?? new MailSender(config_);
        }

        public void Send(IInvoice invoice_, string generatedInvoiceLocation_)
        {
            if (null == invoice_)
                throw new ArgumentNullException(nameof(invoice_));

            if (string.IsNullOrWhiteSpace(invoice_.Client.BillingEmail))
                throw new InvalidOperationException("The client associated with the provided invoice does not have a billing email address defined");

            var mailMessage = Constructor.Create(invoice_, generatedInvoiceLocation_);
            Sender.Send(mailMessage);
        }

        public IEmailInvoiceSenderConfig Config { get; }
        public IMailConstructor Constructor { get; }
        public IMailSender Sender { get; }
    }
}
