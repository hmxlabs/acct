using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Config
{
    public interface IAcctConfig
    {
        string ConnectionString { get; }
        string InvoiceSaveLocation { get; }
        string InvoiceTemplateFile { get; }
        string InvoiceItemTemplateFile { get; }

        IConfigProvider All { get; }
    }
}
