using System;
using CommandLine;
using CommandLine.Text;

namespace HmxLabs.AcctCommandLine
{ 
    public class CommandLineParams : ICommandLineParams
    {
        [Option("settings", Required = false, DefaultValue = null, HelpText = "The location of the settings file")]
        public string ConfigFile { get; set; }

        [Option("invoice-number", Required = true, HelpText = "The invoice number to process")]
        public ulong? InvoiceNumber { get; set; }

        public bool Read(string[] args_)
        {
            if (Parser.Default.ParseArguments(args_, this))
                return InvoiceNumber.HasValue;

            Console.Out.WriteLine(HelpText.AutoBuild(this).ToString());
            return false;
        }
    }
}
