using System;
using System.Collections.Generic;
using System.Globalization;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.Disk
{
    public class CaterAllenTransactionReader : CsvTransactionReader
    {
        public CaterAllenTransactionReader(IAccount account_ = null)
        {
            TransactionAccount = account_;
        }

        public override TransactionFileType Type => TransactionFileType.CaterAllen;

        protected override bool HasHeader => false;
        protected override ITransaction ParseRow(string[] line_)
        {
            // Sample row: 28Jun2017,"R/P to M H Mian","HMXLABS",-732.33,6651.88
            // Best guess at columns is: Transaction Date, Description, Payment Reference, Amount, Running Balance

            var transaction = new Transaction();
            transaction.TransactionDate = DateTime.Parse(line_[0], null, DateTimeStyles.AllowWhiteSpaces);
            transaction.PostDate = transaction.TransactionDate;
            transaction.Description = line_[1] + " REF: " + line_[2];
            transaction.Amount = decimal.Parse(line_[3]);
            transaction.RunningBalance = decimal.Parse(line_[4]);

            transaction.Account = TransactionAccount;
            return transaction;
        }

        protected override void PostProcess(List<ITransaction> transactions_)
        {
            // For some reason the Cater Allen files are reversed and have the oldest
            // transaction at the bottom not the top so we need to invert that.
            transactions_.Reverse();
        }
    }
}
