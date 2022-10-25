using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public class Expense : IExpense
    {
        public const decimal Tolerance = (decimal) 0.00001;

        public ulong ExpenseId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal InvoiceNumber { get; set; }
        public IEntity Supplier { get; set; }
        public string Description { get; set; }
        public decimal AmountPreVat { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalInclVat { get; set; }
        public string Project { get; set; }
        public string PaymentMethod { get; set; }


        public void Validate()
        {
            if (Math.Abs ( AmountPreVat + Vat - TotalInclVat )  > Tolerance )
            {
                throw new ExpenseDataException (ExpenseId, "The amount pre vat plus the vat amount does not equal the total including vat");
            }
                
        }



    }
}
