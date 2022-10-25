using System;

namespace HmxLabs.Acct.Core.Models
{
    public enum AccountType
    {
        CreditCard,
        Current,
        Savings
    }

    public static class AccountTypeExtensions
    {
        public static AccountType ParseAsAccountType(this string accountTypeStr_)
        {
            return (AccountType) Enum.Parse(typeof(AccountType), accountTypeStr_);
        }
    }
}
