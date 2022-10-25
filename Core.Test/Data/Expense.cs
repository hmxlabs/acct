using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public class Expense
    {

        public class Unsent : IExpense
        {
            public static class Values
            {
                public const ulong ExpenseId = 0;
                public static readonly DateTime InvoiceDate = new DateTime(2017, 1, 1);
                public static readonly DateTime PaymentDate = new DateTime(2017, 1, 1);
                public const decimal InvoiceNumber = 0;
                public const string Description = "xxxx";
                public const decimal AmountPreVat = 0;
                public const decimal Vat = 0;
                public const decimal TotalIncVat = 0;
                public const string Project = "xxxx";
                public const string PaymentMethod = "xxxx";
            }

            public static IExpense Instance => new Unsent();

            public ulong ExpenseId => Values.ExpenseId;
            public DateTime InvoiceDate => Values.InvoiceDate;
            public DateTime PaymentDate => Values.PaymentDate;
            public decimal InvoiceNumber => Values.InvoiceNumber;
            public IEntity Supplier => Entity.Acme.Instance;
            public string Description => Values.Description;
            public decimal AmountPreVat => Values.AmountPreVat;
            public decimal Vat => Values.Vat;
            public decimal TotalInclVat => Values.TotalIncVat;
            public string Project => Values.Project;
            public string PaymentMethod => Values.PaymentMethod;
            
            public void Validate()
            {
                throw new NotImplementedException();
            }

            public void Calculate()
            {
                throw new NotImplementedException();
            }
        }

        public class Sent : IExpense
        {
            public static class Values
            {
                public const ulong ExpenseId = 0;
                public static readonly DateTime InvoiceDate = new DateTime(2017, 1, 1);
                public static readonly DateTime PaymentDate = new DateTime(2017, 1, 1);
                public const decimal InvoiceNumber = 0;
                public const string Description = "xxxx";
                public const decimal AmountPreVat = 0;
                public const decimal Vat = 0;
                public const decimal TotalIncVat = 0;
                public const string Project = "xxxx";
                public const string PaymentMethod = "xxxx";
            }

            public static IExpense Instance => new Sent();

            public ulong ExpenseId => Values.ExpenseId;
            public DateTime InvoiceDate => Values.InvoiceDate;
            public DateTime PaymentDate => Values.PaymentDate;
            public decimal InvoiceNumber => Values.InvoiceNumber;
            public IEntity Supplier => Entity.Acme.Instance;
            public string Description => Values.Description;
            public decimal AmountPreVat => Values.AmountPreVat;
            public decimal Vat => Values.Vat;
            public decimal TotalInclVat => Values.TotalIncVat;
            public string Project => Values.Project;
            public string PaymentMethod => Values.PaymentMethod;

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