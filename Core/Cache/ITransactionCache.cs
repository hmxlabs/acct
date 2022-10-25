using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Cache
{
    public interface ITransactionCache
    {
        bool Contains(string keyS_);
        ITransaction Get(string key_);
        void Put(ITransaction transaction_);
    }
}
