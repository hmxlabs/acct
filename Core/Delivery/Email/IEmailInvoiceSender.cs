using HmxLabs.Core.Net.Mail;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public interface IEmailInvoiceSender : IInvoiceSender
    {
        IMailConstructor Constructor { get; }
        IEmailInvoiceSenderConfig Config { get; }

        IMailSender Sender { get; }
    }
}
