using System;
using HmxLabs.Core.DateTIme;

namespace HmxLabs.Acct.Core.Models
{
    public class Transaction : ITransaction
    {
        public Transaction()
        {
            
        }

        public Transaction(ITransaction transaction_)
        {
            if (null == transaction_)
                throw new ArgumentNullException(nameof(transaction_));

            TransactionId = transaction_.TransactionId;
            Account = transaction_.Account;
            TransactionDate = transaction_.TransactionDate;
            PostDate = transaction_.PostDate;
            Description = transaction_.Description;
            Amount = transaction_.Amount;
            RunningBalance = transaction_.RunningBalance;
            Notes = transaction_.Notes;
        }

        public ulong TransactionId { get; set; }
        public IAccount Account { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime PostDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal RunningBalance { get; set; }
        public string Notes { get; set; }
        
        /// <summary>
        /// The key is generated based on the financial data of the transaction that is always available and provided
        /// by the account provider (bank/ credit card company etc). The idea is to provide a key that can used
        /// to attempt to reconcile missing or duplicated transactions.
        /// </summary>
        public string Key => $"{TransactionDate.ToIsoDateString()}||{PostDate.ToIsoDateString()}||{Description.ToLowerInvariant()}||{Amount:F2}";
    }
}
