using CommandLine;

namespace HmxLabs.AcctCommandLine
{
    public class VerbCommandLineParams
    {
        [VerbOption(CommandLineVerbs.SendInvoice, HelpText = "Read an invoice from the database, generate PDF invoice and send via email with HTML summary")]
        public SendInvoiceParams SendInvoice { get; set; }

        [VerbOption(CommandLineVerbs.ImportStatement, HelpText = "Read a statment file from disk and import it to the specified account in the database")]
        public ImportStatementParams ImportStatement { get; set; }
    }
}
