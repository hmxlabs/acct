using System;
using System.IO;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Data.Config;
using HmxLabs.Acct.Core.Test.Data.Html;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.ReportGen.HtmlGen
{
    [TestFixture]
    public class HtmlInvoiceGenTests
    {
        [Test]
        public void TestContructorArgumentGuards()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGen(null));
        }

        [Test]
        public void TestGenerateArgumentGuards()
        {
            var config = new HtmlInvoiceGenConfig(HtmlFileLocations.HtmlTestOutputAbsolutePath, HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            var htmlGen = new HtmlInvoiceGen(config);
            Assert.Throws<ArgumentNullException>(() => htmlGen.Generate(null));
            Assert.Throws<ArgumentNullException>(() => htmlGen.GenerateToDisk(null));
        }

        [Test]
        public void TestStringOutput()
        {
            var config = new HtmlInvoiceGenConfig(HtmlFileLocations.HtmlTestOutputAbsolutePath, HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            var htmlGen = new HtmlInvoiceGen(config);
            var expectedHtml = File.ReadAllText(HtmlFileLocations.UnsentSampleInvoice);
            var generatedOutput = htmlGen.Generate(Invoice.Unsent.Instance);
            Assert.That(generatedOutput, Is.EqualTo(expectedHtml));

            config = new HtmlInvoiceGenConfig(HtmlFileLocations.HtmlTestOutputAbsolutePath, HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen = new HtmlInvoiceGen(config);
            expectedHtml = File.ReadAllText(HtmlFileLocations.SentSampleInvoice);
            generatedOutput = htmlGen.Generate(Invoice.Sent.Instance);
            Assert.That(generatedOutput, Is.EqualTo(expectedHtml));
        }

        [Test]
        public void TestDiskOutput()
        {
            // Delete any existing files before starting the test
            if (Directory.Exists(HtmlFileLocations.HtmlTestOutputAbsolutePath))
                Directory.Delete(HtmlFileLocations.HtmlTestOutputAbsolutePath, true);

            var config = new HtmlInvoiceGenConfig(HtmlFileLocations.HtmlTestOutputAbsolutePath, HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            var htmlGen = new HtmlInvoiceGen(config);

            var outputHtmlFile = htmlGen.GenerateToDisk(Invoice.Unsent.Instance);
            Assert.That(File.Exists(outputHtmlFile), "The stated ouput Html file has not been found on disk");
            var generatedHtml = File.ReadAllText(outputHtmlFile);

            var expectedHtml = File.ReadAllText(HtmlFileLocations.UnsentSampleInvoice);
            Assert.That(generatedHtml, Is.EqualTo(expectedHtml));

            config = new HtmlInvoiceGenConfig(HtmlFileLocations.HtmlTestOutputAbsolutePath, HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen = new HtmlInvoiceGen(config);

            outputHtmlFile = htmlGen.GenerateToDisk(Invoice.Sent.Instance);
            Assert.That(File.Exists(outputHtmlFile), "The stated ouput Html file has not been found on disk");
            generatedHtml = File.ReadAllText(outputHtmlFile);

            expectedHtml = File.ReadAllText(HtmlFileLocations.SentSampleInvoice);
            Assert.That(generatedHtml, Is.EqualTo(expectedHtml));
        }
    }
}
