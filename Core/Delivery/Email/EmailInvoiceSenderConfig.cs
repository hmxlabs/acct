using System;
using HmxLabs.Core.Config;
using HmxLabs.Core.Net.Mail;

namespace HmxLabs.Acct.Core.Delivery.Email
{
    public class EmailInvoiceSenderConfig : MailSenderConfig, IEmailInvoiceSenderConfig
    {
        public static class EmailInvoiceSenderConfigKeys
        {
            public const string SubjectBase = "acct.email.subject";
            public const string AttachmentBaseName = "acct.email.attachment";
            public const string Greeting = "acct.email.greeting";
            public const string Signoff = "acct.email.signoff";
        }

        public EmailInvoiceSenderConfig(IConfigProvider configProvider_) : base(configProvider_)
        {
            if (null == configProvider_)
                throw new ArgumentNullException(nameof(configProvider_));

            SubjectBase = configProvider_.GetConifgAsStringStrict(EmailInvoiceSenderConfigKeys.SubjectBase);
            AttachmentBaseName = configProvider_.GetConifgAsStringStrict(EmailInvoiceSenderConfigKeys.AttachmentBaseName);
            Greeting = configProvider_.GetConifgAsStringStrict(EmailInvoiceSenderConfigKeys.Greeting);
            SignOff = configProvider_.GetConifgAsStringStrict(EmailInvoiceSenderConfigKeys.Signoff);
        }

        public string SubjectBase { get; }
        public string AttachmentBaseName { get; }
        public string Greeting { get; }
        public string SignOff { get; }
    }
}
