using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Cache;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Test.Data;
using NSubstitute;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Cache
{
    [TestFixture]
    public class AccountReadThruCacheTests : AccountCacheTests
    {
        [Test]
        public void TestRequestDoesNotHitPersistenceIfCached()
        {
            var cache = CreateCache();
            cache.Put(Account.CurrentAccount.Instant);
            cache.Get(Account.CurrentAccount.AccountId);

            var persistenceLayer = ((IAccountReadThroughCache) cache).PersistenceLayer;
            persistenceLayer.DidNotReceive().Load(Account.CurrentAccount.AccountId);
        }

        [Test]
        public void TestRquestHitsPersistneceIfNotCached()
        {
            var cache = CreateCache();
            cache.Get(Account.CurrentAccount.AccountId);

            var persistenceLayer = ((IAccountReadThroughCache)cache).PersistenceLayer;
            persistenceLayer.Received(1).Load(Account.CurrentAccount.AccountId);
        }

        protected override IAccountCache CreateCache()
        {
            var cache = new AccountReadThruCache();
            cache.PersistenceLayer = Substitute.For<IAccountPersist>();
            return cache;
        }
    }
}
