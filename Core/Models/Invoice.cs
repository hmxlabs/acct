using System;
using System.Collections.Generic;

namespace HmxLabs.Acct.Core.Models
{
    public class Invoice : IInvoice
    {
        public const decimal Tolerance = (decimal) 0.00001;

        public ulong Number { get; set; }
        public DateTime InvoiceDate { get; set; }
        public IEntity Client { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VatTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public string Project { get; set; }
        public DateTime? PaymentDate { get; set; }
        public InvoiceStatus Status { get; set; }

        public IList<IInvoiceItem> Items => _items;

        public void AddItem(InvoiceItem item_)
        {
            if (null == item_)
                throw new ArgumentNullException(nameof(item_));

            item_.ParentInvoice = this;
            _items.Add(item_);
        }

        public void Validate()
        {
            if (Math.Abs(NetTotal + VatTotal - GrossTotal) > Tolerance)
            {
                throw new InvoiceDataException(Number, "The net total plus the vat amount does not equal the gross total");
            }

            decimal netTotal = 0;
            decimal vatTotal = 0;
            decimal grossTotal = 0;
            foreach (var item in _items)
            {
                item.Validate();
                netTotal += item.NetTotal;
                vatTotal += item.VatAmount;
                grossTotal += item.GrossTotal;
            }

            if (Math.Abs(netTotal - NetTotal) > Tolerance)
            {
                throw new InvoiceDataException(Number, "The net total does not match the computed net total");
            }

            if (Math.Abs(vatTotal - VatTotal) > Tolerance)
            {
                throw new InvoiceDataException(Number, "The VAT total does not match the computed net total");
            }

            if (Math.Abs(grossTotal - GrossTotal) > Tolerance)
            {
                throw new InvoiceDataException(Number, "The gross total does not match the computed net total");
            }
        }

        public void Calculate()
        {
            NetTotal = 0;
            VatTotal = 0;
            GrossTotal = 0;
            foreach (var item in _items)
            {
                item.Calculate();
                NetTotal += item.NetTotal;
                VatTotal += item.VatAmount;
                GrossTotal += item.GrossTotal;
            }
        }

        private readonly List<IInvoiceItem> _items = new List<IInvoiceItem>();
    }
}
