using System;
using System.IO;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using NReco.PdfGenerator;

namespace HmxLabs.Acct.Core.ReportGen.PdfGen
{
    class PdfFromHtmlGenerator : IInvoiceGen
    {
        public PdfFromHtmlGenerator(IHtmlInvoiceGen htmlGen_, string saveLocation_)
        {
            if (null == htmlGen_)
                throw new ArgumentNullException(nameof(htmlGen_));

            if (null == saveLocation_)
                throw new ArgumentNullException(nameof(saveLocation_));

            if (string.IsNullOrWhiteSpace(saveLocation_))
                throw new ArgumentException("Empty or whitespace output directory specifiec", nameof(saveLocation_));

            if (!Directory.Exists(saveLocation_))
                Directory.CreateDirectory(saveLocation_);

            _htmlGen = htmlGen_;
            _converter = CreatePdfTransformer();
            _saveLocation = saveLocation_;
        }

        public string GenerateToDisk(IInvoice invoice_)
        {
            var htmlInvoiceFile = _htmlGen.GenerateToDisk(invoice_);
            var pdfInvoiceFile = Path.Combine(_saveLocation, InvoiceFilename.Generate(invoice_, "pdf"));
            _converter.GeneratePdfFromFile(htmlInvoiceFile, null, pdfInvoiceFile);
            return pdfInvoiceFile;
        }

        private HtmlToPdfConverter CreatePdfTransformer()
        {
            var htmlToPdf = new HtmlToPdfConverter();
            htmlToPdf.Quiet = false;
            htmlToPdf.Size = PageSize.A4;
            htmlToPdf.Zoom = 1.0f;
            htmlToPdf.Margins.Top = 0;
            htmlToPdf.Margins.Left = 0;
            htmlToPdf.Margins.Right = 0;
            htmlToPdf.Margins.Bottom = 0;
            htmlToPdf.CustomWkHtmlPageArgs = "--disable-smart-shrinking";
            htmlToPdf.Orientation = PageOrientation.Portrait;
            return htmlToPdf;
        }

        private readonly IHtmlInvoiceGen _htmlGen;
        private readonly HtmlToPdfConverter _converter;
        private readonly string _saveLocation;
    }
}
