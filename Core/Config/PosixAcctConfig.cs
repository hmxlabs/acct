using System.Collections.Generic;
using System.IO;
using System.Text;
using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Config
{
    public class PosixAcctConfig : IAcctConfig
    {
        public static class ConfigKeys
        {
            public const string ConnectionString = "acct.connectionstring";
            public const string PdfInvoiceSaveLocation = "acct.invoice.pdf.savelocation";
        }

        public static class DefaultFileLocations
        {
            public const string Filename = "acct.config";
            public const string ConfigDirectory = "etc";
            public static readonly string InLocalDirectory = Path.Combine(".", Filename);
            public static readonly string InConfigDirectory = Path.Combine(ConfigDirectory, Filename);
            public static readonly string InPeerConfigDirectory = Path.Combine("..", ConfigDirectory, Filename);
            public static readonly string[] All = {InLocalDirectory, InConfigDirectory, InPeerConfigDirectory};
        }

        public PosixAcctConfig(string filename_ = null)
        {
            ConfigFileRead = LocateConfigFile(filename_);
            var posixReader = new PosixConfigReader(ConfigFileRead);
            ConnectionString = posixReader.GetConifgAsStringStrict(ConfigKeys.ConnectionString);
            InvoicePdfSaveLocation = posixReader.GetConifgAsStringStrict(ConfigKeys.PdfInvoiceSaveLocation);
            All = posixReader;
        }

        private string LocateConfigFile(string filename_)
        {
            var candidateFiles = new List<string>();

            if (!string.IsNullOrWhiteSpace(filename_))
                candidateFiles.Add(filename_);

            candidateFiles.AddRange(DefaultFileLocations.All);
            return LocateDefaultConfigFile(candidateFiles);
        }

        private string LocateDefaultConfigFile(List<string> candidateFilenames_)
        {
            foreach (var filename in candidateFilenames_)
            {
                if (File.Exists(filename))
                    return filename;
            }

            var attemptedLocations = new StringBuilder();
            foreach (var candidateFilename in candidateFilenames_)
            {
                attemptedLocations.AppendLine(candidateFilename);
            }

            throw new FileNotFoundException($"Unable to locate a configuration file in any of the default locations. Attempted the following locations: {attemptedLocations}");
        }

        public string ConfigFileRead { get; }
        public string ConnectionString { get; }
        public string InvoicePdfSaveLocation { get; }
        public IConfigProvider All { get; }
    }
}
