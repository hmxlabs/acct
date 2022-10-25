using System.Collections.Generic;
using HmxLabs.Acct.Core.Cache;

namespace HmxLabs.Acct.Core.Models
{
    public interface ITransactionProcessor
    {
        ITransaction FindOldestTransaction(IEnumerable<ITransaction> transactions_);

        ITransaction FindMostRecentTransaction(IEnumerable<ITransaction> transactions_);

        IDuplicateCheckedTransactions MarkDupicateTransactions(IEnumerable<ITransaction> newTransactions_, ITransactionCache existingTransactions_);
    }
}
