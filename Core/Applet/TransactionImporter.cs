using System.Linq;
using HmxLabs.Acct.Core.Cache;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.Disk;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Core.Log;

namespace HmxLabs.Acct.Core.Applet
{
    public class TransactionImporter : BaseApp
    {
        public TransactionImporter(ILogger logger_ = null, string configFile_ = null) : base(logger_, configFile_)
        {
        }

        public void Run(string accountName_, string inputFile_, TransactionFileType type_)
        {
            Initialise();

            using (var persister = new OleDbPersist(Config.ConnectionString))
            {
                IAccountPersist accountPersist = persister;
                var account = accountPersist.Load(accountName_);
                var transFileReaderFactory = new TransactionFileReaderFactory();
                var transFileReader = transFileReaderFactory.Create(type_, account);
                var newTransactions = transFileReader.Read(inputFile_).ToList();
                var transactionProcessor = new TransactionProcessor();
                var firstTransaction = transactionProcessor.FindOldestTransaction(newTransactions);
                var lastTransaction = transactionProcessor.FindMostRecentTransaction(newTransactions);

                var currentTransactions = new TransactionCache();
                ITransactionPersist transactionPersist = persister;
                currentTransactions.Put(transactionPersist.LoadInPeriod(firstTransaction.TransactionDate, lastTransaction.TransactionDate));
                var checkedTransactions = transactionProcessor.MarkDupicateTransactions(newTransactions, currentTransactions);

                foreach (var transaction in checkedTransactions.UniqueTransactions)
                {
                    transactionPersist.SaveNew(transaction);
                }

                foreach (var transaction in checkedTransactions.DuplicatedTransactions)
                {
                    var markedTransaction = new Transaction(transaction);
                    markedTransaction.Notes = $"**SUSPECTED DUPLICATE TRANSACTION - PLEASE VALIDATE** - {transaction.Notes}";
                    transactionPersist.SaveNew(markedTransaction);
                }
            }
        }
    }
}
