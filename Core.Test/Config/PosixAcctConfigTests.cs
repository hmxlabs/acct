using System.IO;
using HmxLabs.Acct.Core.Config;
using HmxLabs.Acct.Core.Test.Data.Config;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace HmxLabs.Acct.Core.Test.Config
{
    [TestFixture]
    public class PosixAcctConfigTests
    {
        [Test]
        public void TestConstructorArgumentGuard()
        {
            // Passing null means the default file locations should be tried, if nothing is still found then a FileNotFound
            // exception gets thrown as no config was found.
            DeleteAllDefaultConfigFiles(); // delete default config to ensure its not there before running this test
            Assert.Throws<FileNotFoundException>(() => new PosixAcctConfig());
            Assert.Throws<FileNotFoundException>(() => new PosixAcctConfig("non.existent.file"));
        }

        [Test]
        public void TestConfigRead()
        {
            var config = new PosixAcctConfig(ConfigFileLocations.SampleConfig);
            Assert.That(config.ConnectionString, Is.EqualTo(TestConfig.ConfigValues.ConnectionString));
            Assert.That(config.InvoicePdfSaveLocation, Is.EqualTo(TestConfig.ConfigValues.InvoicePdfSaveLocation));
        }

        [Test]
        public void TestDefaultConfigIsFound()
        {
            // create the necessary directory as they won't be there 
            Directory.CreateDirectory("etc");
            Directory.CreateDirectory(Path.Combine("..", "etc"));

            TestDefaultConfigIsFound(Path.Combine(".", "acct.config"));
            TestDefaultConfigIsFound(Path.Combine("etc", "acct.config"));
            TestDefaultConfigIsFound(Path.Combine("..", "etc", "acct.config"));
        }

        private void DeleteAllDefaultConfigFiles()
        {
            if (File.Exists(DefaultConfigFilename))
                File.Delete(DefaultConfigFilename);

            var filenameToDelete = Path.Combine(DefaultConfigDirectory, DefaultConfigFilename);
            if (File.Exists(filenameToDelete))
                File.Delete(filenameToDelete);

            filenameToDelete = Path.Combine("..", DefaultConfigDirectory, DefaultConfigFilename);
            if (File.Exists(filenameToDelete))
                File.Delete(filenameToDelete);
        }

        private void TestDefaultConfigIsFound(string defaultConfig_)
        {
            DeleteAllDefaultConfigFiles();
            File.Copy(ConfigFileLocations.SampleConfig, defaultConfig_, true);
            Assert.That(File.Exists(defaultConfig_));

            var config = new PosixAcctConfig();
            Assert.That(config.ConfigFileRead, Is.EqualTo(defaultConfig_));
        }

        private const string DefaultConfigFilename = "acct.config";
        private const string DefaultConfigDirectory = "etc";
    }
}
