using System;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbAccountPersistTests
    {
        [Test]
        public void TestLoadArgumentGuards()
        {
            IAccountPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            Assert.Throws<ArgumentNullException>(() => persister.Load(null));
        }

        [Test]
        public void TestLoadSingleAccount()
        {
            IAccountPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var account = persister.Load(Account.CurrentAccount.AccountId);
            AccountAssert.AreEqual(Account.CurrentAccount.Instant, account);
        }
    }
}
