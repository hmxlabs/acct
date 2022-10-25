using CommandLine;
using HmxLabs.Acct.Core.Persistence.Disk;

namespace HmxLabs.AcctCommandLine
{
    public class ImportStatementParams : CommonParams
    {
        [Option('t', "type", Required = true, HelpText = "The type of file that is being imported, for example CaterAllen or NatWestCredit")]
        public TransactionFileType Type { get; set; }

        [Option('f', "filename", Required = true, HelpText = "The filename of the file to import")]
        public string Filename { get; set; }

        [Option('a', "accountr", Required = true, HelpText = "The existing account the entries should be added to")]
        public string Account { get; set; }
    }
}
