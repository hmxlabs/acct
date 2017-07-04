using HmxLabs.Acct.Core.Delivery.Email;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Extensions
{
    public static class ConfigAssert
    {
        public static void AssertEquals(IEmailInvoiceSenderConfig reference_, IEmailInvoiceSenderConfig underTest_)
        {
            Assert.That(underTest_.AttachmentBaseName, Is.EqualTo(reference_.AttachmentBaseName));
            Assert.That(underTest_.Greeting, Is.EqualTo(reference_.Greeting));
            Assert.That(underTest_.SignOff, Is.EqualTo(reference_.SignOff));
            Assert.That(underTest_.SubjectBase, Is.EqualTo(reference_.SubjectBase));
            Assert.That(underTest_.SenderName, Is.EqualTo(reference_.SenderName));
            Assert.That(underTest_.Password, Is.EqualTo(reference_.Password));
            Assert.That(underTest_.Port, Is.EqualTo(reference_.Port));
            Assert.That(underTest_.SenderAddress, Is.EqualTo(reference_.SenderAddress));
            Assert.That(underTest_.ServerName, Is.EqualTo(reference_.ServerName));
            Assert.That(underTest_.Username, Is.EqualTo(reference_.Username));
        }
    }
}
