using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public interface IExpense
    {

        ulong ExpenseId { get; }
        DateTime InvoiceDate { get; }
        DateTime PaymentDate { get; }
        decimal InvoiceNumber { get; }
        IEntity Supplier { get; }
        string Description { get; }
        decimal AmountPreVat { get; }
        decimal Vat { get; }
        decimal TotalInclVat { get; }
        string Project { get; }
        string PaymentMethod { get; }
    }
}
