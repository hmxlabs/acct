using System;

namespace HmxLabs.Acct.Core.Models
{
    public interface ITransaction : IKeyed
    {
        ulong TransactionId { get; }
        IAccount Account { get; }
        DateTime TransactionDate { get; }
        DateTime PostDate { get; }
        string Description { get; }
        decimal Amount { get; }
        decimal RunningBalance { get; }
        string Notes { get; }
    }
}
