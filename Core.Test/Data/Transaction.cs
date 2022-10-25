using System;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public class Transaction
    {
        public class Sample : ITransaction
        {
            public const ulong TransactionId = 1;
            public static readonly DateTime TransactionDate = new DateTime(2001, 1, 1);
            public static readonly DateTime PostDate = new DateTime(2001, 1, 2);
            public const string Description = "Transaction description as per the statement";
            public const decimal Amount = 100.50M;
            public const decimal RunningBalance = 500.10M;
            public const string Notes = "Transaction notes as entered by user";
            public const string Key = "2001-01-01||2001-01-02||Transaction description as per the statement||100.50";

            public static ITransaction Instance => new Sample();

            ulong ITransaction.TransactionId => TransactionId;
            IAccount ITransaction.Account => Account.CurrentAccount.Instant;
            DateTime ITransaction.TransactionDate => TransactionDate;
            DateTime ITransaction.PostDate => PostDate;
            string ITransaction.Description => Description;
            decimal ITransaction.Amount => Amount;
            decimal ITransaction.RunningBalance => RunningBalance;
            string ITransaction.Notes => Notes;
            string IKeyed.Key => Key;
        }

        public class CaterAllen : ITransaction
        {
            public const ulong TransactionId = 0;
            public static readonly DateTime TransactionDate = new DateTime(2001, 1, 1);
            public const string Description = "Transaction description REF: Payment Reference";
            public const decimal Amount = -100.50M;
            public const decimal RunningBalance = 500.10M;
            public const string Notes = null;
            public const string Key = "2001-01-01||2001-01-02||Transaction description as per the statement||-100.50";

            public static ITransaction Instance => new CaterAllen();

            ulong ITransaction.TransactionId => TransactionId;
            IAccount ITransaction.Account => Account.CurrentAccount.Instant;
            DateTime ITransaction.TransactionDate => TransactionDate;
            DateTime ITransaction.PostDate => TransactionDate;
            string ITransaction.Description => Description;
            decimal ITransaction.Amount => Amount;
            decimal ITransaction.RunningBalance => RunningBalance;
            string ITransaction.Notes => Notes;
            string IKeyed.Key => Key;
        }

        public class NatWestPdf : ITransaction
        {
            public const ulong TransactionId = 0;
            public static readonly DateTime TransactionDate = new DateTime(2001, 1, 5);
            public static readonly DateTime PostDate = new DateTime(2001, 1, 7);
            public const string Description = "GITHUB.COM HBQWD 4154486673 CA - REF: 48382609";
            public const decimal Amount = -18.69M;
            public const decimal RunningBalance = -230.32M;
            public const string Notes = null;
            public const string Key = "2001-01-05||2001-01-07||GITHUB.COM HBQWD 4154486673 - REF: 48382609||-18.69";

            public static ITransaction Instance => new NatWestPdf();

            ulong ITransaction.TransactionId => TransactionId;
            IAccount ITransaction.Account => Account.CurrentAccount.Instant;
            DateTime ITransaction.TransactionDate => TransactionDate;
            DateTime ITransaction.PostDate => PostDate;
            string ITransaction.Description => Description;
            decimal ITransaction.Amount => Amount;
            decimal ITransaction.RunningBalance => RunningBalance;
            string ITransaction.Notes => Notes;
            string IKeyed.Key => Key;
        }
    }
}
