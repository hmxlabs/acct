using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.OleDb
{
    public class OleDbPersist : IDisposable, IEntityPersist, IInvoiceItemPersist, IInvoicePersist
    {
        public OleDbPersist(string connectionString_)
        {
            if (null == connectionString_)
                throw new ArgumentNullException(nameof(connectionString_));

            if (string.IsNullOrWhiteSpace(connectionString_))
                throw new ArgumentException("The provided connection string is empty", nameof(connectionString_));

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

        private IInvoice ExtractInvoice(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (9 != reader_.FieldCount)
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
            item.Description = reader_.GetStringOrNull(1);
            item.UnitCost = reader_.GetDecimal(2);
            item.Quantity = reader_.GetDecimal(3);
            item.VatRate = reader_.GetDecimal(4);
            item.NetTotal = reader_.GetDecimal(5);
            item.VatAmount = reader_.GetDecimal(6);
            item.GrossTotal = reader_.GetDecimal(7);

            return item;
        }

        private Entity ExtractEntity(OleDbDataReader reader_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (14 != reader_.FieldCount)
                throw new DataException($"Expected to retrieve 13 columns when querying Entity. Retrieved {reader_.FieldCount}");

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

        private readonly OleDbConnection _connection;

        private const string LoadEntityCommand = "SELECT Entity.ID, Entity.Name, Entity.FirstName, Entity.DoorNumber, Entity.Building, Entity.StreetNumber, Entity.StreetName, Entity.City, Entity.PostCode, Entity.Country, Entity.Website, Entity.Phone, Entity.IsCorp, Entity.BillingEmail FROM Entity WHERE Entity.ID = ?";
        private const string LoadInvoiceItemsCommand = "SELECT InvoiceItems.ItemId, InvoiceItems.Description, InvoiceItems.UnitCost, InvoiceItems.Quantity, InvoiceItems.VATRate, InvoiceItems.NetTotal, InvoiceItems.VAT, InvoiceItems.GrossTotal FROM InvoiceItems WHERE InvoiceItems.InvoiceId = ?";
        private const string LoadInvoiceCommand = "SELECT InvoicesWithTotals.InvoiceNumber, InvoicesWithTotals.InvoiceDate, InvoicesWithTotals.ClientId, InvoicesWithTotals.NetTotal, InvoicesWithTotals.VAT, InvoicesWithTotals.GrossTotal, InvoicesWithTotals.PaymentDate, InvoicesWithTotals.Status, InvoicesWithTotals.Project FROM InvoicesWithTotals WHERE InvoicesWithTotals.InvoiceNumber = ?";
    }
}
