using CommandLine;

namespace HmxLabs.AcctCommandLine
{
    public class SendInvoiceParams : CommonParams
    {
        [Option('n', "invoice-number", Required = true, HelpText = "The invoice number to process")]
        public ulong? InvoiceNumber { get; set; }
    }
}
