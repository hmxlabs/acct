using HmxLabs.Acct.Core.Persistence;

namespace HmxLabs.Acct.Core.Cache
{
    public interface IAccountReadThroughCache : IAccountCache
    {
        IAccountPersist PersistenceLayer { get; set; }
    }
}
