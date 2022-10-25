using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Cache
{
    public interface IAccountCache
    {
        bool Contains(string accountId_);
        IAccount Get(string accountId_);    
        void Put(IAccount account_);
    }
}
