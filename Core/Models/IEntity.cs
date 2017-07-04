namespace HmxLabs.Acct.Core.Models
{
    public interface IEntity
    {
        string Id { get; }
        string Name { get; }
        string FirstName { get; }
        string DisplayName { get; }
        IAddress Address { get; }
        string Website { get; }
        string Phone { get; }
        bool IsCorp { get; }
        string BillingEmail { get; }
    }
}
