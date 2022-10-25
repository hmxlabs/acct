using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Cache
{
    public class TransactionCache : KeyedItemCache<ITransaction>, ITransactionCache
    {
    }
}
