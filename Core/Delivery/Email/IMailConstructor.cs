using System.Net.Mail;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public interface IMailConstructor
    {
        MailMessage Create(IInvoice invoice_, string generatedInvoice_);

        IEmailInvoiceSenderConfig Config { get; }
    }
}
