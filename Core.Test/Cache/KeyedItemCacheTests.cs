using System;
using HmxLabs.Acct.Core.Cache;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Cache
{
    [TestFixture]
    public class KeyedItemCacheTests
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

            var item = new TestKeyedItem() {Key = "BLAH"};
            cache.Put(item);
            Assert.Throws<InvalidOperationException>(() => cache.Put(item));
        }

        [Test]
        public void TestPutThenGet()
        {
            var cache = CreateCache();
            const string key = "BLAH";
            var item = new TestKeyedItem() { Key = key };
            cache.Put(item);

            var retrievedItem = cache.Get(key);
            Assert.That(retrievedItem, Is.SameAs(item));
        }

        protected virtual IKeyedItemCache<TestKeyedItem> CreateCache()
        {
            return new KeyedItemCache<TestKeyedItem>();
        }
    }
}
