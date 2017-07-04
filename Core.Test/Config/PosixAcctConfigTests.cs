using System;
using System.IO;
using HmxLabs.Acct.Core.Config;
using HmxLabs.Acct.Core.Test.Data.Config;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace HmxLabs.Acct.Core.Test.Config
{
    [TestFixture]
    public class PosixAcctConfigTests
    {
        [Test]
        public void TestConstructorArgumentGuard()
        {
            Assert.Throws<ArgumentNullException>(() => new PosixAcctConfig(null));
            Assert.Throws<ArgumentException>(() => new PosixAcctConfig(""));
            Assert.Throws<FileNotFoundException>(() => new PosixAcctConfig("non.existent.file"));
        }

        [Test]
        public void TestConfigRead()
        {
            var config = new PosixAcctConfig(ConfigFileLocations.SampleConfig);
            Assert.That(config.ConnectionString, Is.EqualTo(TestConfig.ConfigValues.ConnectionString));
            Assert.That(config.InvoiceSaveLocation, Is.EqualTo(TestConfig.ConfigValues.InvoiceSaveLocation));
            Assert.That(config.InvoiceTemplateFile, Is.EqualTo(TestConfig.ConfigValues.InvoiceTemplateFile));
            Assert.That(config.InvoiceItemTemplateFile, Is.EqualTo(TestConfig.ConfigValues.InvoiceItemTemplateFile));
        }
    }
}
