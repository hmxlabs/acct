using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;
using HmxLabs.Acct.Core.Models;


namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
        [TestFixture]
        public class OleDbExpensePersistTests
        {
            [Test]
            public void TestLoadSingleExpense()
            {
                IExpensePersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
                var expense = persister.Load(HmxLabs.Acct.Core.Test.Data.Expense.Unsent.Values.ExpenseId);
                ExpensesAssert.AreEqual(Core.Test.Data.Expense.Unsent.Instance, expense);
            }
        
            [Test]
            public void TestLoadExpenseWithoutPaymentDate()
            {
                IExpensePersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
                var expense = persister.Load(Core.Test.Data.Expense.Sent.Values.ExpenseId);
            ExpensesAssert.AreEqual(Core.Test.Data.Expense.Sent.Instance, expense);
            }
        }
    }
