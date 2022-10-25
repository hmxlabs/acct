using System;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Extensions;
using HmxLabs.Core.Config;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Delivery.Email
{
    [TestFixture]
    public class EmailHtmlBodyGenConfigTests
    {
        [Test]
        public void TestConstructorArgumentGuards()
        {
            // ReSharper disable ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new EmailHtmlBodyGenConfig(null));

            Assert.Throws<ArgumentNullException>(() => new EmailHtmlBodyGenConfig(null, "item.file"));
            Assert.Throws<ArgumentNullException>(() => new EmailHtmlBodyGenConfig("template.file", null));

            Assert.Throws<ArgumentException>(() => new EmailHtmlBodyGenConfig("template.file", "  "));
            Assert.Throws<ArgumentException>(() => new EmailHtmlBodyGenConfig("", "item.file"));
            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void TestReadSampleConfig()
        {
            var posixConfigReader = new PosixConfigReader(ConfigFileLocations.SampleConfig);
            var emailSenderConfig = new EmailHtmlBodyGenConfig(posixConfigReader);
            ConfigAssert.AssertEquals(TestEmailHtmlBodyGenConfig.Instance, emailSenderConfig);
        }
    }
}
