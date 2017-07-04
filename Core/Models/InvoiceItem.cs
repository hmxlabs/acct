using System;

namespace HmxLabs.Acct.Core.Models
{
    public class InvoiceItem : IInvoiceItem
    {
        public IInvoice ParentInvoice { get; set; }
        public ulong Id { get; set; }
        public string Description { get; set;  }
        public decimal UnitCost { get; set;  }
        public decimal Quantity { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal GrossTotal { get; set; }
        public void Validate()
        {
            var netTotal = UnitCost*Quantity;
            if (Math.Abs(netTotal - NetTotal) > Invoice.Tolerance)
            {
                throw new InvoiceItemDataException(Id, "The net total value is invalid");
            }

            var vatAmount = NetTotal*VatRate;
            if (Math.Abs(vatAmount - VatAmount) > Invoice.Tolerance)
            {
                throw new InvoiceItemDataException(Id, "The VAT amount is invalid");
            }

            var grossTotal = NetTotal + VatAmount;
            if (Math.Abs(grossTotal - GrossTotal) > Invoice.Tolerance)
            {
                throw new InvoiceItemDataException(Id, "The gross total is invalid");
            }
        }

        public void Calculate()
        {
            NetTotal = UnitCost*Quantity;
            VatAmount = NetTotal*VatRate;
            GrossTotal = NetTotal + VatAmount;
        }
    }
}
