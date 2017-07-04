using System;
using System.Globalization;
using System.IO;
using System.Text;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Core.Base;
using HmxLabs.Core.Html;

namespace HmxLabs.Acct.Core.ReportGen.HtmlGen
{
    public class HtmlInvoiceGen : IInvoiceGen
    {
        public HtmlInvoiceGen(string invoiceTemplateFile_, string invoiceItemTemplateFile_)
        {
            if (null == invoiceItemTemplateFile_)
                throw new ArgumentNullException(nameof(invoiceItemTemplateFile_));

            if (null == invoiceTemplateFile_)
                throw new ArgumentNullException(nameof(invoiceTemplateFile_));

            if (string.IsNullOrWhiteSpace(invoiceTemplateFile_))
                throw new ArgumentException("An empty invoice template file was provided", nameof(invoiceTemplateFile_));

            if (string.IsNullOrWhiteSpace(invoiceItemTemplateFile_))
                throw new ArgumentException("An empty invoice item template file was provided", nameof(invoiceItemTemplateFile_));

            if (!File.Exists(invoiceTemplateFile_))
                throw new FileNotFoundException($"The specified invoice template file [{invoiceTemplateFile_}] does not exist");

            if (!File.Exists(invoiceItemTemplateFile_))
                throw new FileNotFoundException($"The specified invoice item template file [{invoiceItemTemplateFile_}] does not exist");

            _invoiceTemplate = File.ReadAllText(invoiceTemplateFile_);
            _invoiceItemTemplate = File.ReadAllText(invoiceItemTemplateFile_);
        }

        public string OutputPath { get; set; }
        public string Generate(IInvoice invoice_)
        {
            if (null == invoice_)
                throw new ArgumentNullException(nameof(invoice_));

            var htmlEditor = new DreamweaverSubstituter(_invoiceTemplate);
            htmlEditor.UpdateEditRegionValue(InvoiceTags.ClientName, invoice_.Client.DisplayName);
            htmlEditor.UpdateEditRegionValue(InvoiceTags.ClientAddress, GenerateClientAddress(invoice_.Client.Address));
            htmlEditor.UpdateEditRegionValue(InvoiceTags.InvoiceDate, invoice_.InvoiceDate.ToLongDateString());
            htmlEditor.UpdateEditRegionValue(InvoiceTags.Number, invoice_.Number.ToString());
            htmlEditor.UpdateEditRegionValue(InvoiceTags.TotalHighlight, invoice_.GrossTotal.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(InvoiceTags.NetTotal, invoice_.NetTotal.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(InvoiceTags.VatTotal, invoice_.VatTotal.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(InvoiceTags.GrossTotal, invoice_.GrossTotal.ToCurrencyString());

            var items = new StringBuilder();
            items.Append(Literals.NewLine);
            foreach (var invoiceItem in invoice_.Items)
            {
                items.AppendLine(GenerateInvoiceItem(invoiceItem));
            }
            items.Append(Literals.InvoiceItemIndent);

            htmlEditor.UpdateEditRegionValue(InvoiceTags.ItemList, items.ToString());
            return htmlEditor.Html;
        }

        public string GenerateToDisk(IInvoice invoice_)
        {
            if (null == invoice_)
                throw new ArgumentNullException(nameof(invoice_));

            if (null == OutputPath)
                throw new InvalidOperationException("No OutputDirectory has been set");

            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);

            var filename = Path.Combine(OutputPath, GenerateFilename(invoice_));
            File.WriteAllText(filename, Generate(invoice_));
            return filename;
        }

        private string GenerateFilename(IInvoice invoice_)
        {
            return $"{invoice_.InvoiceDate.ToIsoDateString()}--{invoice_.Number}--{invoice_.Client.DisplayName}.html";
        }

        private string GenerateClientAddress(IAddress address_)
        {
            var output = new StringBuilder();            

            if (null != address_.DoorNumber)
            {
                output.Append(address_.DoorNumber);
                output.Append(Literals.Space);
                if (null == address_.Building)
                {
                    output.Append(Html.Break);
                    output.Append(Literals.NewLine);
                    output.Append(Literals.AddressIndent);
                }
            }

            if (null != address_.Building)
            {
                output.Append(address_.Building);
                output.Append(Html.Break);
                output.Append(Literals.NewLine);
                output.Append(Literals.AddressIndent);
            }

            if (null != address_.StreetNumber)
            {
                output.Append(address_.StreetNumber);
                output.Append(Literals.Space);
                if (null == address_.StreetName)
                {
                    output.Append(Html.Break);
                    output.Append(Literals.NewLine);
                    output.Append(Literals.AddressIndent);
                }
            }

            if (null != address_.StreetName)
            {
                output.Append(address_.StreetName);
                output.Append(Html.Break);
                output.Append(Literals.NewLine);
                output.Append(Literals.AddressIndent);
            }

            if (null != address_.City)
            {
                output.Append(address_.City);
                output.Append(Html.Break);
                output.Append(Literals.NewLine);
                output.Append(Literals.AddressIndent);
            }

            if (null != address_.PostCode)
            {
                output.Append(address_.PostCode);
                output.Append(Html.Break);
                output.Append(Literals.NewLine);
                output.Append(Literals.AddressIndent);
            }

            if (null != address_.Country)
            {
                output.Append(address_.Country);
                output.Append(Literals.NewLine);
                output.Append(Literals.AddressIndent);
            }

            return output.ToString();
        }

        private string GenerateInvoiceItem(IInvoiceItem invoiceItem_)
        {
            var htmlEditor = new DreamweaverSubstituter(_invoiceItemTemplate);
            htmlEditor.UpdateEditRegionValue(ItemTags.Description, invoiceItem_.Description);
            htmlEditor.UpdateEditRegionValue(ItemTags.Quantity, invoiceItem_.Quantity.ToNumberString());
            htmlEditor.UpdateEditRegionValue(ItemTags.UnitPrice, invoiceItem_.UnitCost.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(ItemTags.VaTRate, invoiceItem_.VatRate.ToPercentageString());
            htmlEditor.UpdateEditRegionValue(ItemTags.NetAmount, invoiceItem_.NetTotal.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(ItemTags.VaTAmount, invoiceItem_.VatAmount.ToCurrencyString());
            htmlEditor.UpdateEditRegionValue(ItemTags.GrossAmount, invoiceItem_.GrossTotal.ToCurrencyString());

            return htmlEditor.Html;
        }

        private static class Literals
        {
            public const string Space = " ";
            public const string NewLine = "\r\n";
            public const string AddressIndent = "\t\t\t\t";
            public const string InvoiceItemIndent = "\t\t\t\t\t";
        }

        private static class Html
        {
            public const string Break = "<br/>";
        }

        private static class InvoiceTags
        {
            public const string ClientName = "ClientName";
            public const string ClientAddress = "ClientAddress";
            public const string InvoiceDate = "InvoiceDate";
            public const string Number = "InvoiceNumber";
            public const string TotalHighlight = "InvoiceTotalHighlight";
            public const string ItemList = "InvoiceItemList";
            public const string NetTotal = "InvoiceNetAmount";
            public const string VatTotal = "InvoiceVATAmount";
            public const string GrossTotal = "InvoiceTotal";

            
        }

        public static class ItemTags
        {
            public const string Description = "InvoiceItemDescription";
            public const string Quantity = "InvoiceItemQuantity";
            public const string UnitPrice = "InvoiceItemUnitPrice";
            public const string VaTRate = "InvoiceItemVATRate";
            public const string NetAmount = "InvoiceItemNetAmount";
            public const string VaTAmount = "InvoiceItemVATAmount";
            public const string GrossAmount = "InvoiceItemGrossAmount";
        }

        private readonly string _invoiceTemplate;
        private readonly string _invoiceItemTemplate;
    }

    public static class NumberStringFormatter
    {
        public static string ToCurrencyString(this decimal input_)
        {
            return Math.Round(input_, 2).ToString("##,###.00");
        }

        public static string ToNumberString(this decimal input_)
        {
            return Math.Round(input_, 2).ToString("##,###.##");
        }

        public static string ToPercentageString(this decimal input_)
        {
            return $"{(Math.Round(input_, 2)*100).ToString("##.##")}%";
        }
    }
}
