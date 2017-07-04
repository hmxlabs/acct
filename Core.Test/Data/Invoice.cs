using System;
using System.Collections.Generic;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public class Invoice
    {
        public class Unsent : IInvoice
        {
            public static class Values
            {
                public const ulong Number = 4;
                public static readonly DateTime InvoiceDate = new DateTime(2017, 1, 1);
                public const decimal NetTotal = 80.585M;
                public const decimal VatTotal = 16.117M;
                public const decimal GrossTotal = 96.702M;
                public const string Project = "Startup";
                public static readonly DateTime PaymentDate = new DateTime(2017, 01, 05);
                public const InvoiceStatus Status = InvoiceStatus.New;
            }

            public static IInvoice Instance => new Unsent();

            public ulong Number => Values.Number;
            public DateTime InvoiceDate => Values.InvoiceDate;
            public IEntity Client => Entity.Acme.Instance;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatTotal => Values.VatTotal;
            public decimal GrossTotal => Values.GrossTotal;
            public string Project => Values.Project;
            public DateTime? PaymentDate => Values.PaymentDate;
            public InvoiceStatus Status => Values.Status;
            public IList<IInvoiceItem> Items => new[] {InvoiceItem.Widgets.Instance, InvoiceItem.DoohDahs.Instance};
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class Sent : IInvoice
        {
            public static class Values
            {
                public const ulong Number = 5;
                public static readonly DateTime InvoiceDate = new DateTime(2017, 6, 1);
                public const decimal NetTotal = 12000M;
                public const decimal VatTotal = 1800M;
                public const decimal GrossTotal = 13800M;
                public const string Project = "Startup";
                public static readonly DateTime? PaymentDate = null;
                public const InvoiceStatus Status = InvoiceStatus.Sent;
            }

            public static IInvoice Instance => new Sent();

            public ulong Number => Values.Number;
            public DateTime InvoiceDate => Values.InvoiceDate;
            public IEntity Client => Entity.Acme.Instance;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatTotal => Values.VatTotal;
            public decimal GrossTotal => Values.GrossTotal;
            public string Project => Values.Project;
            public DateTime? PaymentDate => Values.PaymentDate;
            public InvoiceStatus Status => Values.Status;
            public IList<IInvoiceItem> Items => new[] { InvoiceItem.Gadgets.Instance };
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class NoBillingData : IInvoice
        {
            public static class Values
            {
                public const ulong Number = 6;
                public static readonly DateTime InvoiceDate = new DateTime(2016, 7, 1);
                public const decimal NetTotal = 450M;
                public const decimal VatTotal = 90M;
                public const decimal GrossTotal = 540M;
                public const string Project = "Startup";
                public static readonly DateTime? PaymentDate = null;
                public const InvoiceStatus Status = InvoiceStatus.New;
            }

            public static IInvoice Instance => new NoBillingData();

            public ulong Number => Values.Number;
            public DateTime InvoiceDate => Values.InvoiceDate;
            public IEntity Client => Entity.NoBill.Instance;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatTotal => Values.VatTotal;
            public decimal GrossTotal => Values.GrossTotal;
            public string Project => Values.Project;
            public DateTime? PaymentDate => Values.PaymentDate;
            public InvoiceStatus Status => Values.Status;
            public IList<IInvoiceItem> Items => new[] { InvoiceItem.Bananas.Instance };
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }
    }
}
