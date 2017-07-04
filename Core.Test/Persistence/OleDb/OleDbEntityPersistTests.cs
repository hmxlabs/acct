using System;
using HmxLabs.Acct.Core.Persistence;
using HmxLabs.Acct.Core.Persistence.OleDb;
using HmxLabs.Acct.Core.Test.Data;
using HmxLabs.Acct.Core.Test.Extensions;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbEntityPersistTests
    {
        [Test]
        public void TestLoadArgumentChecks()
        {
            IEntityPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            Assert.Throws<ArgumentNullException>(() => persister.Load(null));
        }

        [Test]
        public void TestLoadSingleEntity()
        {
            IEntityPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var entity = persister.Load(Entity.Literal.EntityId);
            EntityAssert.AreEqual(Entity.Literal.Instance, entity);
        }

        [Test]
        public void TestLoadSingleEntityWithNullItems()
        {
            IEntityPersist persister = new OleDbPersist(OleDbPersistTests.TestDatabaseConnectionString);
            var entity = persister.Load(Entity.Acme.EntityId);
            EntityAssert.AreEqual(Entity.Acme.Instance, entity);
        }
    }
}
