using System.IO;

namespace HmxLabs.Acct.Core.Test.Data.Config
{
    public class ConfigFileLocations
    {
        public static readonly string ConfigDirectoryRelativePath = Path.Combine(".", "Data", "Config");
        public static readonly string ConfigDirectoryAbsolutePath = Path.GetFullPath(ConfigDirectoryRelativePath);

        public const string SampleConfigFilename = "acct.config";
        public static readonly string SampleConfig = Path.Combine(ConfigDirectoryAbsolutePath, SampleConfigFilename);
    }
}
