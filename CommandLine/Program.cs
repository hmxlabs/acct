using System;
using HmxLabs.Acct.Core.Applet;
using HmxLabs.Core.Log;

namespace HmxLabs.AcctCommandLine
{
    class Program
    {
        public static void Main(string[] args_)
        {
            ILogger logger = null;
            try
            {
                logger = new ConsoleLogger("default");
                logger.Open();
                logger.Info("Starting Acct CLI...");
                var cliParams = new CommandLineParams();
                if (!cliParams.Read(args_))
                {
                    if (!cliParams.InvoiceNumber.HasValue)
                        logger.Error("No value for the invoice number was supplied");
                    else
                        logger.Error("Unable to correctly parse the command line options. Please see help output above");

                    return;
                }

                logger.Notice("Using settings from file: ", cliParams.ConfigFile);
                var senderApplet = new InvoiceSender(logger, cliParams.ConfigFile);
                senderApplet.Run(cliParams.InvoiceNumber.Value);
            }
            catch (Exception exp)
            {
                if (null == logger)
                    Console.WriteLine($"Something went very wrong! Sorry about that. The details are below:\r\n{exp}");
                else
                    logger.Fatal(exp, "Something went very wrong! Sorry about that. The details are below");
            }
        }
    }
}
