using System;
using System.Collections.Generic;
using System.Linq;
using HmxLabs.Acct.Core.Cache;

namespace HmxLabs.Acct.Core.Models
{
    public class TransactionProcessor : ITransactionProcessor
    {
        public ITransaction FindOldestTransaction(IEnumerable<ITransaction> transactions_)
        {
            if (null == transactions_)
                throw new ArgumentNullException(nameof(transactions_), "No transactions provided");

            var transactionList = transactions_.ToList();
            if (0 == transactionList.Count)
                throw new ArgumentException("The provided transaction enumeration is empty");

            ITransaction oldestTransaction = transactionList[0];
            foreach (var transaction in transactionList)
            {
                if (0 > transaction.TransactionDate.CompareTo(oldestTransaction.TransactionDate))
                {
                    oldestTransaction = transaction;
                }
            }

            return oldestTransaction;
        }

        public ITransaction FindMostRecentTransaction(IEnumerable<ITransaction> transactions_)
        {
            if (null == transactions_)
                throw new ArgumentNullException(nameof(transactions_), "No transactions provided");

            var transactionList = transactions_.ToList();
            if (0 == transactionList.Count)
                throw new ArgumentException("The provided transaction enumeration is empty");

            ITransaction mostRecentTransaction = transactionList[0];
            foreach (var transaction in transactionList)
            {
                if (0 < transaction.TransactionDate.CompareTo(mostRecentTransaction.TransactionDate))
                {
                    mostRecentTransaction = transaction;
                }
            }

            return mostRecentTransaction;
        }

        public IDuplicateCheckedTransactions MarkDupicateTransactions(IEnumerable<ITransaction> newTransactions_, ITransactionCache existingTransactions_)
        {
            var duplicateTransactions = new List<ITransaction>();
            var uniqueTransactions = new List<ITransaction>();

            foreach (var newTransaction in newTransactions_)
            {
                if (existingTransactions_.Contains(newTransaction.Key))
                    duplicateTransactions.Add(newTransaction);
                else
                    uniqueTransactions.Add(newTransaction);
            }

            return new DuplicateCheckedTransactions(duplicateTransactions, uniqueTransactions);
        }
    }
}
