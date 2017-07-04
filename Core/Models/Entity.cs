namespace HmxLabs.Acct.Core.Models
{
    public class Entity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public IAddress Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public bool IsCorp { get; set; }
        public string BillingEmail { get; set; }

        public string DisplayName => string.IsNullOrWhiteSpace(FirstName) ? Name : $"{FirstName} {Name}";
    }
}
