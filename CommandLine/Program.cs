using System;
using HmxLabs.Acct.Core.Applet;
using HmxLabs.Core.Log;

namespace HmxLabs.AcctCommandLine
{
    class Program
    {
        public static void Main(string[] args_)
        {
            var app = new Program();
            app.RunMain(args_);
        }

        private void RunMain(string[] args_)
        {
            _logger = null;
            try
            {
                _logger = new ConsoleLogger("default");
                _logger.Open();
                _logger.Info("Starting Acct CLI...");

                var commandLineParams = new VerbCommandLineParams();
                var parseStatus = CommandLine.Parser.Default.ParseArguments(args_, commandLineParams, OnParse);
                if (!parseStatus)
                {
                    _logger.Error("Unable to correctly parse the command line options. Please see help output above");
                    Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
                }
            }
            catch (Exception exp)
            {
                if (null == _logger)
                    Console.WriteLine($"Something went very wrong! Sorry about that. The details are below:\r\n{exp}");
                else
                    _logger.Fatal(exp, "Something went very wrong! Sorry about that. The details are below");
            }
        }

        private void OnParse(string verb_, object params_)
        {
            switch (verb_)
            {
                case CommandLineVerbs.SendInvoice:
                    OnSendInvoice((SendInvoiceParams)params_);
                    break;

                case CommandLineVerbs.ImportStatement:
                    OnImportStatement((ImportStatementParams)params_);
                    break;

                default:
                    _logger.Fatal($"Unrecognised command [{verb_}]. Unable to proceed");
                    break;
            }
        }

        private void OnSendInvoice(SendInvoiceParams params_)
        {
            var senderApplet = new InvoiceSender(_logger, params_.ConfigFile);
            if (params_.InvoiceNumber.HasValue)
                senderApplet.Run(params_.InvoiceNumber.Value);
            else
                _logger.Error("No value for the invoice number was supplied");
        }

        private void OnImportStatement(ImportStatementParams params_)
        {
            var importerApplet = new TransactionImporter(_logger, params_.ConfigFile);
            importerApplet.Run(params_.Account, params_.Filename, params_.Type);
        }
        private ILogger _logger;
    }
}
