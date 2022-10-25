using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Config;
using HmxLabs.Core.Log;

namespace HmxLabs.Acct.Core.Applet
{
    public abstract class BaseApp
    {
        protected BaseApp(ILogger logger_ = null, string configFile_ = null)
        {
            Logger = logger_;
            ConfigFile = configFile_;
        }

        public ILogger Logger { get; set; }
        public string ConfigFile { get; set; }

        protected PosixAcctConfig Config { get; set; }

        protected void Initialise()
        {
            if (null == Logger)
            {
                Logger = new ConsoleLogger("Default");
                Logger.Notice("No logger was provided. Using a default console logger");
            }
            
            Config = new PosixAcctConfig(ConfigFile);
            Logger.Info($"Using configuration from {Config.ConfigFileRead}");
        }
    }
}
