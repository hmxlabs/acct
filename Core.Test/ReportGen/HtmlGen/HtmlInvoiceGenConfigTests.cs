using System;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Extensions;
using HmxLabs.Core.Config;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.ReportGen.HtmlGen
{
    [TestFixture]
    class HtmlInvoiceGenConfigTests
    {
        [Test]
        public void TestConstructorArgumentGuard()
        {
            // ReSharper disable ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGenConfig(null));
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGenConfig(null, "invoicefile.template", "invoiceitemfile.template"));
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGenConfig("output.file", null, "invoiceitemfile.template"));
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGenConfig("output.file", "invoicefile.template", null));
            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void TestConfigProviderConstructorArgumentGuards()
        {
            var config = new FixedConfigProvider();
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.SaveLocation, null);
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.TemplateFile, "template.file");
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.ItemTemplateFile, "templateitem.file");
            // ReSharper disable ObjectCreationAsStatement
            Assert.Throws<ConfigException>(() => new HtmlInvoiceGenConfig(config));
            // ReSharper restore ObjectCreationAsStatement

            config = new FixedConfigProvider();
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.SaveLocation, "outputDir");
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.TemplateFile, null);
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.ItemTemplateFile, "templateitem.file");
            // ReSharper disable ObjectCreationAsStatement
            Assert.Throws<ConfigException>(() => new HtmlInvoiceGenConfig(config));
            // ReSharper restore ObjectCreationAsStatement

            config = new FixedConfigProvider();
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.SaveLocation, "outputDir");
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.TemplateFile, "template.file");
            config.AddConfig(HtmlInvoiceGenConfig.HtmlInvoiceGenConfigKeys.ItemTemplateFile, null);
            // ReSharper disable ObjectCreationAsStatement
            Assert.Throws<ConfigException>(() => new HtmlInvoiceGenConfig(config));
            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void TestConfigRead()
        {
            var posixConfigReader = new PosixConfigReader(ConfigFileLocations.SampleConfig);
            var config = new HtmlInvoiceGenConfig(posixConfigReader);
            ConfigAssert.AssertEquals(TestHtmlInvoiceGenConfig.Instance, config);
        }
    }
}
