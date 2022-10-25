using System.Collections.Generic;

namespace HmxLabs.Acct.Core.Models
{
    public interface IDuplicateCheckedTransactions
    {
        IEnumerable<ITransaction> DuplicatedTransactions { get; }
        IEnumerable<ITransaction> UniqueTransactions { get; }
    }
}
