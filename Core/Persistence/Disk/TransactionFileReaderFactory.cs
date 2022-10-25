using System;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.Disk
{
    public class TransactionFileReaderFactory
    {
        public ITransactionFileReader Create(TransactionFileType type_, IAccount account_)
        {
            if (TransactionFileType.CaterAllen == type_)
                return new CaterAllenTransactionReader(account_);

            if (TransactionFileType.NatWestCreditPdf == type_)
                return new NatWestCreditCardPdfReader(account_);

            throw new InvalidOperationException("Unsupported type of transaction reader requested");
        }
    }
}
