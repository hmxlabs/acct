namespace HmxLabs.Acct.Core.Models
{
    public class Account : IAccount
    {
        public string Key => AccountId;
        public string AccountId { get; set; }
        public string Provider { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
