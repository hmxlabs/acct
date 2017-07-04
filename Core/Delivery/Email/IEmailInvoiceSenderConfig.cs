using HmxLabs.Core.Net.Mail;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public interface IEmailInvoiceSenderConfig : IMailSenderConfig
    {
        string SubjectBase { get; }
        string AttachmentBaseName { get; }
        string Greeting { get; }
        string SignOff { get; }
    }
}
