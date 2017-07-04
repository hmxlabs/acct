using System;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Extensions;
using HmxLabs.Core.Config;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Delivery.Email
{
    [TestFixture]
    public class EmailInvoiceSenderConfigTests
    {
        [Test]
        public void TestConstructorArgumentGuards()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new EmailInvoiceSenderConfig(null));
        }

        [Test]
        public void TestConfigLoad()
        {
            var posixConfigReader = new PosixConfigReader(ConfigFileLocations.SampleConfig);
            var emailSenderConfig = new EmailInvoiceSenderConfig(posixConfigReader);
            ConfigAssert.AssertEquals(TestConfig.Instance, emailSenderConfig);
        }
    }
}
