using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public static class Account
    {
        public class CurrentAccount : IAccount
        {
            public const string AccountId = "AccountID";
            public const string Provider = "Provider";
            public const string AccountNumber = "Account#";
            public const AccountType Type = AccountType.Current;
            public const string Currency = "CCY";
            public const string Description = "Account description for current account";

            public static IAccount Instant { get; } = new CurrentAccount();


            string IKeyed.Key => AccountId;
            string IAccount.AccountId => AccountId;
            string IAccount.Provider => Provider;
            string IAccount.AccountNumber => AccountNumber;
            AccountType IAccount.Type => Type;
            string IAccount.Currency => Currency;
            string IAccount.Description => Description;
        }
    }
}
