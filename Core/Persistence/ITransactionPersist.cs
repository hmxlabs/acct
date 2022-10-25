using System;
using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface ITransactionPersist
    {
        IEnumerable<ITransaction> LoadInPeriod(DateTime startDate_, DateTime endDate_);
        void SaveNew(ITransaction transaction_);
        void Update(ITransaction transaction_);
    }
}
