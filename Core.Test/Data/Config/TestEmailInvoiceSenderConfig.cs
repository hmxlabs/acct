using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Delivery.Email;

namespace HmxLabs.Acct.Core.Test.Data.Config
{
    public class TestEmailInvoiceSenderConfig : IEmailInvoiceSenderConfig
    {
        public static class ConfigValues
        {
            public const string SmtpServer = "mail.acme.com";
            public const int SmtpPort = 90009;
            public const string SmtpUsername = "road.runner@acme";
            public const string SmtpPassword = "iLoveBirdSeed";
            public const string SenderEmail = "wiley.coyote@acme.com";
            public const string SenderName = "Wiley Coyote Esq";
            public const string SubjectBase = "Acme Corp Invoice";
            public const string AttachmentName = "Acme-Invoice";
            public const string Greeting = "What's up doc?";
            public const string SignOff = "That's all folks!";
            public const bool EnableSsl = true;

            public static readonly NetworkCredential SmtpCredentials = new NetworkCredential(SmtpUsername, SmtpPassword);
            public static readonly MailAddress Sender = new MailAddress(SenderEmail, SenderName);
        }

        public static TestEmailInvoiceSenderConfig Instance => new TestEmailInvoiceSenderConfig();

        public string ServerName => ConfigValues.SmtpServer;
        public int Port => ConfigValues.SmtpPort;
        public string Username => ConfigValues.SmtpUsername;
        public string Password => ConfigValues.SmtpPassword;
        public NetworkCredential UserCredentials => ConfigValues.SmtpCredentials;
        public MailAddress Sender => ConfigValues.Sender;
        public string SenderAddress => ConfigValues.SenderEmail;
        public string SenderName => ConfigValues.SenderName;
        public string SubjectBase => ConfigValues.SubjectBase;
        public string AttachmentBaseName => ConfigValues.AttachmentName;
        public string Greeting => ConfigValues.Greeting;
        public string SignOff => ConfigValues.SignOff;
        public bool EnableSsl => ConfigValues.EnableSsl;
    }
}
