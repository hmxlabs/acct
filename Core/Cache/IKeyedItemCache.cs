using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Cache
{
    public interface IKeyedItemCache<T> where T : IKeyed
    {
        bool Contains(string key_);

        void Put(T item_);

        T Get(string key_);
    }
}
