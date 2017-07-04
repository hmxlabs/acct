using System;
using System.IO;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Acct.Core.Test.Data;
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
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGen(null, null));
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, null));
            Assert.Throws<ArgumentNullException>(() => new HtmlInvoiceGen(null, HtmlFileLocations.InvoiceItemTemplateFilename));
            Assert.Throws<FileNotFoundException>(() => new HtmlInvoiceGen("Non.Existent.html", HtmlFileLocations.InvoiceItemTemplate));
            Assert.Throws<FileNotFoundException>(() => new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, "non.existent.html"));
        }

        [Test]
        public void TestGenerateArgumentGuards()
        {
            var htmlGen = new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            Assert.Throws<ArgumentNullException>(() => htmlGen.Generate(null));
            Assert.Throws<ArgumentNullException>(() => htmlGen.GenerateToDisk(null));
            
            // This next call should throw because we haven't set an output location.
            Assert.Throws<InvalidOperationException>(() => htmlGen.GenerateToDisk(Invoice.Unsent.Instance));
        }

        [Test]
        public void TestStringOutput()
        {
            var htmlGen = new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen.OutputPath = HtmlFileLocations.HtmlTestOutputAbsolutePath;
            var expectedHtml = File.ReadAllText(HtmlFileLocations.UnsentSampleInvoice);
            var generatedOutput = htmlGen.Generate(Invoice.Unsent.Instance);
            Assert.That(generatedOutput, Is.EqualTo(expectedHtml));

            htmlGen = new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen.OutputPath = HtmlFileLocations.HtmlTestOutputAbsolutePath;
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

            var htmlGen = new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen.OutputPath = HtmlFileLocations.HtmlTestOutputAbsolutePath;

            var outputHtmlFile = htmlGen.GenerateToDisk(Invoice.Unsent.Instance);
            Assert.That(File.Exists(outputHtmlFile), "The stated ouput Html file has not been found on disk");
            var generatedHtml = File.ReadAllText(outputHtmlFile);

            var expectedHtml = File.ReadAllText(HtmlFileLocations.UnsentSampleInvoice);
            Assert.That(generatedHtml, Is.EqualTo(expectedHtml));

            htmlGen = new HtmlInvoiceGen(HtmlFileLocations.InvoiceTemplate, HtmlFileLocations.InvoiceItemTemplate);
            htmlGen.OutputPath = HtmlFileLocations.HtmlTestOutputAbsolutePath;

            outputHtmlFile = htmlGen.GenerateToDisk(Invoice.Sent.Instance);
            Assert.That(File.Exists(outputHtmlFile), "The stated ouput Html file has not been found on disk");
            generatedHtml = File.ReadAllText(outputHtmlFile);

            expectedHtml = File.ReadAllText(HtmlFileLocations.SentSampleInvoice);
            Assert.That(generatedHtml, Is.EqualTo(expectedHtml));
        }
    }
}
