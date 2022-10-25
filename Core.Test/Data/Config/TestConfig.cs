using System;
using System.Net;
using System.Net.Mail;
using HmxLabs.Acct.Core.Config;
using HmxLabs.Acct.Core.Delivery.Email;
using HmxLabs.Acct.Core.ReportGen.HtmlGen;
using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Test.Data.Config
{
    public class TestConfig : IAcctConfig
    {
        public static class ConfigValues
        {
            public const string InvoicePdfSaveLocation = "C:\\acct\\invoices\\pdf";
            public const string ConnectionString = "My Access DB Connection String";
        }

        public static TestConfig Instance => new TestConfig();

        public string ConnectionString => ConfigValues.ConnectionString;
        public string InvoicePdfSaveLocation => ConfigValues.InvoicePdfSaveLocation;
        public IConfigProvider All => throw new NotImplementedException();
    }
}
