using System;
using HmxLabs.Acct.Core.Persistence.OleDb;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace HmxLabs.Acct.Core.Test.Persistence.OleDb
{
    [TestFixture]
    public class OleDbPersistTests
    {
        public const string TestDatabaseConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\Db\\TestData.accdb;";

        [Test]
        public void TestConstructorArgumentChecks()
        {
            Assert.Throws<ArgumentNullException>(() => new OleDbPersist(null));
            Assert.Throws<ArgumentException>(() => new OleDbPersist(string.Empty));
            Assert.Throws<ArgumentException>(() => new OleDbPersist("   "));
        }

        [Test]
        public void TestConnectionStringPassThrough()
        {
            var connection = new OleDbPersist(TestDatabaseConnectionString);
            Assert.That(TestDatabaseConnectionString, Is.EqualTo(connection.ConnectionString));
        }
    }
}
