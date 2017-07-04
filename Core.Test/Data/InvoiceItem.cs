using System;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public static class InvoiceItem
    {
        public class Widgets : IInvoiceItem
        {
            public static class Values
            {
                public const ulong InvoiceId = 4;
                public const ulong Id = 6;
                public const string Description = "Widgets and gizmos with bells and whistles";
                public const decimal UnitCost = 12.75M;
                public const decimal Quantity = 3.5M;
                public const decimal NetTotal = 44.625M;
                public const decimal VatRate = 0.2M;
                public const decimal VatAmount = 8.925M;
                public const decimal GrossTotal = 53.55M;
            }

            public static IInvoiceItem Instance { get; } = new Widgets();

            public IInvoice ParentInvoice { get; }
            public ulong Id => Values.Id;
            public string Description => Values.Description;
            public decimal UnitCost => Values.UnitCost;
            public decimal Quantity => Values.Quantity;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatRate => Values.VatRate;
            public decimal VatAmount => Values.VatAmount;
            public decimal GrossTotal => Values.GrossTotal;
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class DoohDahs : IInvoiceItem
        {
            public static class Values
            {
                public const ulong InvoiceId = 4;
                public const ulong Id = 7;
                public const string Description = "Doodahs and fingums";
                public const decimal UnitCost = 8.99M;
                public const decimal Quantity = 4M;
                public const decimal NetTotal = 35.96M;
                public const decimal VatRate = 0.2M;
                public const decimal VatAmount = 7.192M;
                public const decimal GrossTotal = 43.152M;
            }

            public static IInvoiceItem Instance { get; } = new DoohDahs();

            public IInvoice ParentInvoice { get; }
            public ulong Id => Values.Id;
            public string Description => Values.Description;
            public decimal UnitCost => Values.UnitCost;
            public decimal Quantity => Values.Quantity;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatRate => Values.VatRate;
            public decimal VatAmount => Values.VatAmount;
            public decimal GrossTotal => Values.GrossTotal;
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class Gadgets : IInvoiceItem
        {
            public static class Values
            {
                public const ulong InvoiceId = 5;
                public const ulong Id = 8;
                public const string Description = "Gadgets";
                public const decimal UnitCost = 800M;
                public const decimal Quantity = 15.00000M;
                public const decimal NetTotal = 12000M;
                public const decimal VatRate = 0.15M;
                public const decimal VatAmount = 1800M;
                public const decimal GrossTotal = 13800M;
            }

            public static IInvoiceItem Instance { get; } = new Gadgets();

            public IInvoice ParentInvoice { get; }
            public ulong Id => Values.Id;
            public string Description => Values.Description;
            public decimal UnitCost => Values.UnitCost;
            public decimal Quantity => Values.Quantity;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatRate => Values.VatRate;
            public decimal VatAmount => Values.VatAmount;
            public decimal GrossTotal => Values.GrossTotal;
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class Bananas : IInvoiceItem
        {
            public static class Values
            {
                public const ulong InvoiceId = 6;
                public const ulong Id = 9;
                public const string Description = "Bananas";
                public const decimal UnitCost = 150M;
                public const decimal Quantity = 3M;
                public const decimal NetTotal = 450M;
                public const decimal VatRate = 0.2M;
                public const decimal VatAmount = 90M;
                public const decimal GrossTotal = 540M;
            }

            public static IInvoiceItem Instance { get; } = new Gadgets();

            public IInvoice ParentInvoice { get; }
            public ulong Id => Values.Id;
            public string Description => Values.Description;
            public decimal UnitCost => Values.UnitCost;
            public decimal Quantity => Values.Quantity;
            public decimal NetTotal => Values.NetTotal;
            public decimal VatRate => Values.VatRate;
            public decimal VatAmount => Values.VatAmount;
            public decimal GrossTotal => Values.GrossTotal;
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
