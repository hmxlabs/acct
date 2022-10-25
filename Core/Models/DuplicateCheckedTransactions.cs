using System;
using System.Collections.Generic;

namespace HmxLabs.Acct.Core.Models
{
    public class DuplicateCheckedTransactions : IDuplicateCheckedTransactions
    {
        public DuplicateCheckedTransactions(IEnumerable<ITransaction> duplicates_, IEnumerable<ITransaction> unique_)
        {
            DuplicatedTransactions = duplicates_ ?? throw new ArgumentNullException(nameof(duplicates_));
            UniqueTransactions = unique_ ?? throw new ArgumentNullException(nameof(unique_));
        }

        public IEnumerable<ITransaction> DuplicatedTransactions { get; }
        public IEnumerable<ITransaction> UniqueTransactions { get; }
    }
}
