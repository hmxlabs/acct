using System;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Persistence;

namespace HmxLabs.Acct.Core.Cache
{
    public class AccountReadThruCache : IAccountReadThroughCache
    {
        public bool Contains(string accountId_)
        {
            return _cache.Contains(accountId_);
        }

        public IAccount Get(string accountId_)
        {
            if (_cache.Contains(accountId_))
                return _cache.Get(accountId_);

            if (null == PersistenceLayer)
                throw new InvalidOperationException("No peristence layer provided to read through cache");

            return PersistenceLayer.Load(accountId_);
        }

        public void Put(IAccount account_)
        {
            _cache.Put(account_);
        }

        public IAccountPersist PersistenceLayer { get; set; }

        private readonly AccountCache _cache = new AccountCache();
    }
}
