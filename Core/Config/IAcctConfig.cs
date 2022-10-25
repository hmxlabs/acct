using HmxLabs.Core.Config;

namespace HmxLabs.Acct.Core.Config
{
    public interface IAcctConfig
    {
        string ConnectionString { get; }
        string InvoicePdfSaveLocation { get; }
        IConfigProvider All { get; }
    }
}
