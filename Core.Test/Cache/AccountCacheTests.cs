using System;
using HmxLabs.Acct.Core.Cache;
using HmxLabs.Acct.Core.Test.Data;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Cache
{
    [TestFixture]
    public class AccountCacheTests
    {
        [Test]
        public void TestArgumentGuardsOnContains()
        {
            var cache = CreateCache();
            Assert.Throws<ArgumentNullException>(() => cache.Contains(null));
            Assert.Throws<ArgumentException>(() => cache.Contains(""));
            Assert.Throws<ArgumentException>(() => cache.Contains(" \t\r"));
        }
            
        [Test]
        public void TestArgumentGuardsOnGet()
        {
            var cache = CreateCache();
            Assert.Throws<ArgumentNullException>(() => cache.Get(null));
            Assert.Throws<ArgumentException>(() => cache.Get(""));
            Assert.Throws<ArgumentException>(() => cache.Get(" \t\r"));
        }

        [Test]
        public void TestArgumentGuardsOnPut()
        {
            var cache = CreateCache();
            Assert.Throws<ArgumentNullException>(() => cache.Put(null));

            cache.Put(Account.CurrentAccount.Instant);
            Assert.Throws<InvalidOperationException>(() => cache.Put(Account.CurrentAccount.Instant));
        }

        [Test]
        public void TestPutThenGet()
        {
            var cache = CreateCache();
            var account = Account.CurrentAccount.Instant;
            cache.Put(account);

            var retrievedAccount = cache.Get(account.AccountId);
            Assert.That(retrievedAccount, Is.SameAs(account));
        }

        protected virtual IAccountCache CreateCache()
        {
            return new AccountCache();
        }
    }
}
