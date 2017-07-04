using System;
using System.IO;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Data.Html;
using HmxLabs.Core.Net.Mail;
using NSubstitute;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Delivery.Email
{
    [TestFixture]
    public class EMailInvoiceSenderTests
    {
        public void TestConstructorArgumentGuards()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new EmailInvoiceSender(null));
        }

        [Test]
        public void TestConstructorCreatesDefaultMailConstructorAndSender()
        {
            var sender = new EmailInvoiceSender(TestConfig.Instance);
            Assert.That(sender.Constructor, Is.Not.Null);
            Assert.That(sender.Constructor.GetType() == typeof(MailConstructor));
            Assert.That(sender.Sender, Is.Not.Null);
            Assert.That(sender.Sender.GetType() == typeof(MailSender));
        }

        [Test]
        public void TestSendArgumentGuards()
        {
            var sender = new EmailInvoiceSender(TestConfig.Instance);
            Assert.Throws<ArgumentNullException>(() => sender.Send(null, "sdfdskfjd"));
            Assert.Throws<ArgumentNullException>(() => sender.Send(Invoice.Sent.Instance, null));
            Assert.Throws<ArgumentException>(() => sender.Send(Invoice.Sent.Instance, string.Empty));
            Assert.Throws<FileNotFoundException>(() => sender.Send(Invoice.Sent.Instance, "non.existent.file.html"));
        }

        [Test]
        public void TestSendWithClientWithoutBillingEmail()
        {
            var mailConstructor = Substitute.For<IMailConstructor>();
            var mailSender = Substitute.For<IMailSender>();
            var sender = new EmailInvoiceSender(TestConfig.Instance, mailConstructor, mailSender);

            Assert.Throws<InvalidOperationException>(() => sender.Send(Invoice.NoBillingData.Instance, HtmlFileLocations.UnsentSampleInvoice));
        }

        [Test]
        public void TestSendCreatesNewMailMessage()
        {
            var mailConstructor = Substitute.For<IMailConstructor>();
            var mailSender = Substitute.For<IMailSender>();
            var sender = new EmailInvoiceSender(TestConfig.Instance, mailConstructor, mailSender);

            sender.Send(Invoice.Unsent.Instance, HtmlFileLocations.UnsentSampleInvoice);
            mailConstructor.ReceivedWithAnyArgs(1).Create(null, null);
        }

        [Test]
        public void TestSendCallMailSenderToSend()
        {
            var mailConstructor = Substitute.For<IMailConstructor>();
            var mailSender = Substitute.For<IMailSender>();
            var sender = new EmailInvoiceSender(TestConfig.Instance, mailConstructor, mailSender);

            sender.Send(Invoice.Unsent.Instance, HtmlFileLocations.UnsentSampleInvoice);
            mailSender.ReceivedWithAnyArgs(1).Send(null);
        }
    }
}
