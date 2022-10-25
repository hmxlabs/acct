using System;
using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Cache
{
    public class KeyedItemCache<T> : IKeyedItemCache<T> where T : IKeyed
    {
        public bool Contains(string accountId_)
        {
            if (null == accountId_)
                throw new ArgumentNullException(nameof(accountId_));

            if (string.IsNullOrWhiteSpace(accountId_))
                throw new ArgumentException("Empty or whitespace key provided", nameof(accountId_));

            return _items.ContainsKey(accountId_);
        }

        public T Get(string accountId_)
        {
            if (null == accountId_)
                throw new ArgumentNullException(nameof(accountId_));

            if (string.IsNullOrWhiteSpace(accountId_))
                throw new ArgumentException("Empty or whitespace key provided", nameof(accountId_));

            return _items[accountId_];
        }

        public void Put(T item_)
        {
            if (null == item_)
                throw new ArgumentNullException(nameof(item_));

            if (_items.ContainsKey(item_.Key))
                throw new InvalidOperationException($"An item with the provided key [{item_.Key}] already exists within the cache");

            _items[item_.Key] = item_;
        }

        public void Put(IEnumerable<T> items_)
        {
            if (null == items_)
                throw new ArgumentNullException(nameof(items_), "Null items provided");

            foreach (var item in items_)
            {
                Put(item);
            }
        }

        private readonly Dictionary<string, T> _items = new Dictionary<string, T>();
    }
}
