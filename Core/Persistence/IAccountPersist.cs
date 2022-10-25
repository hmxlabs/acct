using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface IAccountPersist
    {
        IAccount Load(string accountId_);
    }
}
