using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface IEntityPersist
    {
        IEntity Load(string id_);
        void Save(IEntity entity_);
    }
}
