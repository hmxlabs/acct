using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.Disk
{
    public interface ITransactionFileReader
    {
        IAccount TransactionAccount { get; set; }

        IEnumerable<ITransaction> Read(string filename_);

        TransactionFileType Type { get; }
    }
}
