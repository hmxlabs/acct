namespace HmxLabs.Acct.Core.Models
{
    public interface IAccount : IKeyed
    {
        string AccountId { get; }
        string Provider { get; }
        string AccountNumber { get; }
        AccountType Type { get; }
        string Currency { get; }
        string Description { get; }
    }
}