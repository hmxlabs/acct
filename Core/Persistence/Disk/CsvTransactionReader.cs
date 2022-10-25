using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.Disk
{
    public abstract class CsvTransactionReader : ITransactionFileReader
    {
        public const string DefaultDelimiter = ",";

        public string Delimiter { get; protected set; }

        public IAccount TransactionAccount { get; set; }

        public abstract TransactionFileType Type { get; }

        public IEnumerable<ITransaction> Read(string filename_)
        {
            if (null == filename_)
                throw new ArgumentNullException(nameof(filename_));

            if (string.IsNullOrWhiteSpace(filename_))
                throw new ArgumentException("The provided filename is empty or whitespace", nameof(filename_));

            if (!File.Exists(filename_))
                throw new FileNotFoundException($"The specified transaction input file [{filename_}] was not found");

            if (null == TransactionAccount)
                throw new InvalidOperationException("An account to which the transactions belong has not been specified");

            var config = GetCsvConfig();
            var transactions = new List<ITransaction>();
            using (var reader = new StreamReader(filename_))
            {
                var csvReader = new CsvReader(reader, config);
                while (csvReader.Read())
                {
                    var transaction = ParseRow(csvReader.CurrentRecord);
                    if (null != transaction)
                        transactions.Add(transaction);
                }
            }

            PostProcess(transactions);

            return transactions;
        }

        protected CsvTransactionReader()
        {
            Delimiter = DefaultDelimiter;
        }

        protected abstract bool HasHeader { get; }

        protected abstract ITransaction ParseRow(string[] line_);

        protected virtual void PostProcess(List<ITransaction> transactions_)
        {
            // Default is a no op
        }

        protected virtual CsvConfiguration GetCsvConfig()
        {
            var config = new CsvConfiguration();
            config.TrimFields = true;
            config.HasHeaderRecord = HasHeader;
            config.Delimiter = Delimiter;
            return config;
        }
    }
}
