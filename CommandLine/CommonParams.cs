using CommandLine;

namespace HmxLabs.AcctCommandLine
{
    public class CommonParams
    {
        [Option("settings", Required = false, DefaultValue = null, HelpText = "The location of the settings file")]
        public string ConfigFile { get; set; }
    }
}
