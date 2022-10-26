using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using HmxLabs.Acct.Core.Cache;
using HmxLabs.Acct.Core.Models;
using HmxLabs.Acct.Core;

namespace HmxLabs.Acct.Core.Persistence.OleDb
{
    public class OleDbPersist : IDisposable, IEntityPersist, IInvoiceItemPersist, IInvoicePersist, ITransactionPersist, IAccountPersist, IExpensePersist
    {
        public OleDbPersist(string connectionString_)
        {
            if (null == connectionString_)
                throw new ArgumentNullException(nameof(connectionString_));

            if (string.IsNullOrWhiteSpace(connectionString_))
                throw new ArgumentException("The provided connection string is empty", nameof(connectionString_));

            _accountCache = new AccountReadThruCache();
            _accountCache.PersistenceLayer = this;

            _connection = new OleDbConnection(connectionString_);
            _connection.Open();
        }

        ~OleDbPersist()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string ConnectionString => _connection.ConnectionString;


        public Entity LoadEntity(string id_)
        {
            if (null == id_)
                throw new ArgumentNullException(nameof(id_));

            if (string.IsNullOrWhiteSpace(id_))
                throw new ArgumentException("The provided entity ID is empty", nameof(id_));

            using (var command = new OleDbCommand(LoadEntityCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("EntityId", OleDbType.VarChar)).Value = id_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader oject returned when attempting to query Entity. SQL: [{LoadEntityCommand}], Entity ID [{id_}]");

                    if (!reader.HasRows)
                        throw new DataException($"Expected to retrieve 1 row when querying Entity but no rows were returned. SQL: [{LoadEntityCommand}], Entity ID [{id_}]");

                    reader.Read();
                    return ExtractEntity(reader);
                }
            }
        }

        IAccount IAccountPersist.Load(string accountId_)
        {
            if (null == accountId_)
                throw new ArgumentNullException(nameof(accountId_));

            if (string.IsNullOrWhiteSpace(accountId_))
                throw new ArgumentException("The provided account Id is null or empty", nameof(accountId_));

            using (var command = new OleDbCommand(LoadAccountCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("AccountId", OleDbType.VarChar)).Value = accountId_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader object returned when attempting to query Accounts. SQL: [{LoadAccountCommand}], Account ID [{accountId_}]");

                    if (!reader.HasRows)
                        throw new DataException($"Expected to retrieve 1 row when querying Accounts but no rows were returned. SQL: [{LoadAccountCommand}], Account ID [{accountId_}]");

                    reader.Read();
                    return ExtractAccount(reader);
                }
            }
        }

        IEntity IEntityPersist.Load(string id_)
        {
            return LoadEntity(id_);
        }

        void IEntityPersist.Save(IEntity entity_)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoiceItem> LoadInvoiceItems(ulong invoiceId_)
        {
            using (var command = new OleDbCommand(LoadInvoiceItemsCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("InvoiceId", OleDbType.UnsignedBigInt)).Value = invoiceId_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader object returned when attempting to query invoice item. SQL: [{LoadInvoiceItemsCommand}], Entity ID [{invoiceId_}]");

                    if (!reader.HasRows)
                        throw new DataException($"Expeected to retrieve at least 1 row when querying InvoiceItems but no rows were returned. SQL: [{LoadInvoiceItemsCommand}], Entity ID [{invoiceId_}]");

                    var items = new List<InvoiceItem>();
                    while (reader.Read())
                    {
                        items.Add(ExtractInvoiceItem(reader));
                    }
                    return items;
                }
            }
        }

        IEnumerable<IInvoiceItem> IInvoiceItemPersist.Load(ulong invoiceId_)
        {
            return LoadInvoiceItems(invoiceId_);
        }

        void IInvoiceItemPersist.Save(IInvoiceItem invoiceItem_)
        {
            throw new NotImplementedException();
        }

        IInvoice IInvoicePersist.Load(ulong number_)
        {
            using (var command = new OleDbCommand(LoadInvoiceCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("InvoiceNumber", OleDbType.UnsignedBigInt)).Value = number_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader object returned when attempting to query invoice item. SQL: [{LoadInvoiceCommand}], Invoice Number [{number_}]");

                    if (!reader.HasRows)
                        throw new DataException($"Expeected to retrieve 1 row when querying InvoicesWithTotals but no rows were returned. SQL: [{LoadInvoiceCommand}], Invoice Number [{number_}]");

                    reader.Read();
                    return ExtractInvoice(reader);
                }
            }
        }

        void IInvoicePersist.Save(IInvoice invoice_)
        {
            throw new NotImplementedException();
        }

        void IInvoicePersist.MarkAsSent(ulong id_)
        {
            throw new NotImplementedException();
        }

		private void Dispose(bool disposing_)	
        {
            if (disposing_)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        
		IEnumerable<ITransaction> ITransactionPersist.LoadInPeriod(DateTime startDate_, DateTime endDate_)
        {
            using (var command = new OleDbCommand(LoadTransactionsCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("StartDate", OleDbType.Date, Int32.MaxValue, "TransactionDate")).Value = startDate_;
                command.Parameters.Add(new OleDbParameter("EndDate", OleDbType.Date, Int32.MaxValue, "TransactionDate")).Value = endDate_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader object returned when attempting to query invoice item. SQL: [{LoadTransactionsCommand}], Start Date [{startDate_}], End Date [{endDate_}]");

                    var items = new List<ITransaction>();

                    if (!reader.HasRows)
                        return items;

                    while (reader.Read())
                    {
                        items.Add(ExtractTransaction(reader));
                    }
                    return items;
                }
            }
        }

        void ITransactionPersist.SaveNew(ITransaction transaction_)
        {
            if (null == transaction_)
                throw new ArgumentNullException(nameof(transaction_));

            if (null == transaction_.Account)
                throw new ArgumentException($"Transaction ID [{transaction_.TransactionId}], Description [{transaction_.Description}] does not have an account and can not be persisted");

            using (var command = new OleDbCommand(SaveNewTransaction, _connection))
            {
                //Account, TransactionDate, PostDate, Description, Amount, RunningBalance, Notes
                command.Parameters.Add(new OleDbParameter("Account", OleDbType.VarChar)).Value = transaction_.Account.AccountId;
                command.Parameters.Add(new OleDbParameter("TransactionDate", OleDbType.Date)).Value = transaction_.TransactionDate;
                command.Parameters.Add(new OleDbParameter("PostDate", OleDbType.Date)).Value = transaction_.PostDate;
                command.Parameters.Add(new OleDbParameter("Description", OleDbType.VarChar)).Value = transaction_.Description;
                command.Parameters.Add(new OleDbParameter("Amount", OleDbType.Decimal)).Value = transaction_.Amount;
                command.Parameters.Add(new OleDbParameter("RunningBalance", OleDbType.Decimal)).Value = transaction_.RunningBalance;
                command.Parameters.Add(new OleDbParameter("Notes", OleDbType.VarChar)).Value = transaction_.Notes ?? string.Empty;
                command.ExecuteNonQuery();
            }
        }

        void ITransactionPersist.Update(ITransaction transaction_)
        {
            if (null == transaction_)
                throw new ArgumentNullException(nameof(transaction_));

            throw new NotImplementedException();
        }

 		public IExpense Load(ulong id_)
        {

            using (var command = new OleDbCommand(LoadExpenseCommand, _connection))
            {
                command.Parameters.Add(new OleDbParameter("EntityId", OleDbType.VarChar)).Value = id_;
                using (var reader = command.ExecuteReader())
                {
                    if (null == reader)
                        throw new DataException($"Null reader object returned when attempting to query Expenses. SQL: [{LoadEntityCommand}], Expense ID [{id_}]");

                    if (!reader.HasRows)
                        throw new DataException($"Expected to retrieve 1 row when querying Expenses but no rows were returned. SQL: [{LoadEntityCommand}], Expense ID [{id_}]");

                    reader.Read();
                    return (Expense)ExtractExpenses(reader);
                }
            }
        }

        private IExpense ExtractExpenses(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (11 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 11 columns when querying Expenses. Retrieved {reader_.FieldCount}");

            var expense = new Expense();

            expense.ExpenseId = (ulong) reader_.GetValue(0);
            expense.InvoiceDate = reader_.GetDateTime(1);
            expense.PaymentDate = reader_.GetDateTime(2);
            expense.InvoiceNumber = reader_.GetDecimal(3);
            expense.Supplier = (IEntity)reader_.GetValue(4);
            expense.Description = reader_.GetStringOrNull(5);
            expense.AmountPreVat = reader_.GetDecimal(6);
            expense.Vat = reader_.GetDecimal(7);
            expense.TotalInclVat = reader_.GetDecimal(8);
            expense.Project = reader_.GetStringOrNull(9);
            expense.PaymentMethod = reader_.GetStringOrNull(10);

            return expense;

        }

        private IInvoice ExtractInvoice(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (10 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 9 columns when querying InvoiceItems. Retrieved {reader_.FieldCount}");

            var invoice = new Invoice();
            var tempNumber = (int) reader_.GetValue(0);
            invoice.Number = (ulong) tempNumber;
            invoice.InvoiceDate = reader_.GetDateTime(1);
            var clientId = reader_.GetString(2);
            invoice.NetTotal = reader_.GetDecimal(3);
            invoice.VatTotal = reader_.GetDecimal(4);
            invoice.GrossTotal = reader_.GetDecimal(5);
            invoice.PaymentDate = reader_.GetNullableDateTime(6);
            var status = reader_.GetString(7);
            invoice.Project = reader_.GetStringOrNull(8);
            invoice.PurchaseOrder = reader_.GetStringOrNull(9);

            var items = LoadInvoiceItems(invoice.Number);
            foreach (var item in items)
            {
                invoice.AddItem(item);
            }

            invoice.Client = LoadEntity(clientId);
            invoice.Status = InvoiceStatusExtensions.Parse(status);

            return invoice;
        }

        private InvoiceItem ExtractInvoiceItem(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (8 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 8 columns when querying InvoiceItems. Retrieved {reader_.FieldCount}");

            var item = new InvoiceItem();
            var tempId = (int)reader_.GetValue(0);
            item.Id = (ulong) tempId;
            //item.Id = (ulong) reader_.GetInt64(0);
            item.Description = reader_.GetStringOrNull(1);
            item.UnitCost = reader_.GetDecimal(2);
            item.Quantity = reader_.GetDecimal(3);
            item.VatRate = reader_.GetDecimal(4);
            item.NetTotal = reader_.GetDecimal(5);
            item.VatAmount = reader_.GetDecimal(6);
            item.GrossTotal = reader_.GetDecimal(7);

            return item;
        }

        private Transaction ExtractTransaction(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (8 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 8 columns when querying Transactions. Retrieved {reader_.FieldCount}");

            var transaction = new Transaction();
            var tempId = (int) reader_.GetValue(0);
            transaction.TransactionId = (ulong) tempId;

            var accountId = reader_.GetStringOrNull(1);
            if (null != accountId)
                transaction.Account = _accountCache.Get(accountId);

            transaction.TransactionDate = reader_.GetDateTime(2);
            transaction.PostDate = reader_.GetDateTime(3);
            transaction.Description = reader_.GetStringOrNull(4);
            transaction.Amount = reader_.GetDecimal(5);
            transaction.RunningBalance = reader_.GetDecimal(6);
            transaction.Notes = reader_.GetStringOrNull(7);

            return transaction;
        }

        private Entity ExtractEntity(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (14 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 14 columns when querying Entity. Retrieved {reader_.FieldCount}");

            var entity = new Entity();
            var address = new Address();
            entity.Address = address;

            entity.Id = reader_.GetStringOrNull(0);
            entity.Name = reader_.GetStringOrNull(1);
            entity.FirstName = reader_.GetStringOrNull(2);

            address.DoorNumber = reader_.GetStringOrNull(3);
            address.Building = reader_.GetStringOrNull(4);
            address.StreetNumber = reader_.GetStringOrNull(5);
            address.StreetName = reader_.GetStringOrNull(6);
            address.City = reader_.GetStringOrNull(7);
            address.PostCode = reader_.GetStringOrNull(8);
            address.Country = reader_.GetStringOrNull(9);

            entity.Website = reader_.GetStringOrNull(10);
            entity.Phone = reader_.GetStringOrNull(11);
            entity.IsCorp = reader_.GetBoolean(12);
            entity.BillingEmail = reader_.GetStringOrNull(13);

            return entity;
        }

        private Account ExtractAccount(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (6 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 6 columns when querying Accounts. Retrieved {reader_.FieldCount}");

            var account = new Account();

            account.AccountId = reader_.GetStringOrNull(0);
            account.Provider = reader_.GetStringOrNull(1);
            account.AccountNumber = reader_.GetStringOrNull(2);
            var accountType = reader_.GetStringOrNull(3);
            if (!string.IsNullOrWhiteSpace(accountType))
                account.Type = accountType.ParseAsAccountType();

            account.Currency = reader_.GetStringOrNull(4);
            account.Description = reader_.GetStringOrNull(5);

            return account;
        }
        
    public void Save(IExpense expense_)
        {
            throw new NotImplementedException();
        }    

        private readonly OleDbConnection _connection;
        private readonly IAccountReadThroughCache _accountCache;

        private const string LoadEntityCommand = "SELECT Entity.ID, Entity.Name, Entity.FirstName, Entity.DoorNumber, Entity.Building, Entity.StreetNumber, Entity.StreetName, Entity.City, Entity.PostCode, Entity.Country, Entity.Website, Entity.Phone, Entity.IsCorp, Entity.BillingEmail FROM Entity WHERE Entity.ID = ?";
        private const string LoadInvoiceItemsCommand = "SELECT InvoiceItems.ItemId, InvoiceItems.Description, InvoiceItems.UnitCost, InvoiceItems.Quantity, InvoiceItems.VATRate, InvoiceItems.NetTotal, InvoiceItems.VAT, InvoiceItems.GrossTotal FROM InvoiceItems WHERE InvoiceItems.InvoiceId = ? ORDER BY InvoiceItems.ItemId";
        private const string LoadInvoiceCommand = "SELECT InvoicesWithTotals.InvoiceNumber, InvoicesWithTotals.InvoiceDate, InvoicesWithTotals.ClientId, InvoicesWithTotals.NetTotal, InvoicesWithTotals.VAT, InvoicesWithTotals.GrossTotal, InvoicesWithTotals.PaymentDate, InvoicesWithTotals.Status, InvoicesWithTotals.Project, InvoicesWithTotals.PurchaseOrder FROM InvoicesWithTotals WHERE InvoicesWithTotals.InvoiceNumber = ?";
        private const string LoadTransactionsCommand = "SELECT Transactions.TransactionId, Transactions.Account, Transactions.TransactionDate, Transactions.PostDate, Transactions.Description, Transactions.Amount, Transactions.RunningBalance, Transactions.Notes FROM Transactions WHERE Transactions.TransactionDate >= ? AND Transactions.TransactionDate <= ?";
        private const string LoadAccountCommand = "SELECT Accounts.AccountId, Accounts.Provider, Accounts.AccountNumber, Accounts.Type, Accounts.Currency, Accounts.Description FROM Accounts WHERE Accounts.AccountId = ?";

        private const string SaveNewTransaction = "INSERT INTO Transactions(Account, TransactionDate, PostDate, Description, Amount, RunningBalance, Notes) VALUES(?, ?, ?, ?, ?, ?, ?)";

		private const string LoadExpenseCommand = "SELECT Expenses.ExpenseID, Expenses.InvoiceDate, Expenses.PaymentDate, Expenses.InvoiceNumber, Expenses.Supplier, Expenses.Description, Expenses.Amount(preVAT), Expenses.VAT, Expenses.Total(inclVAT), Expenses.Project, Expenses.PaymentMethod, Expenses.Notes FROM Expenses WHERE Expenses.ExpenseID = ?";
    }
}
