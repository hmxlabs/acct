using HmxLabs.Acct.Core.Models;
using NUnit.Framework;

namespace HmxLabs.Acct.Core.Test.Models
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void TestDisplayNameConstructionWithoutFirstName()
        {
            var entity = new Entity();
            entity.Name = Name;
            entity.FirstName = null;
            Assert.That(entity.DisplayName, Is.EqualTo(AcmeName));

            entity.FirstName = string.Empty;
            Assert.That(entity.DisplayName, Is.EqualTo(AcmeName));

            entity.FirstName = "  \t";
            Assert.That(entity.DisplayName, Is.EqualTo(AcmeName));
        }

        [Test]
        public void TestDisplayNameConstructionWithFirstName()
        {
            var entity = new Entity();
            entity.Name = AltName;
            entity.FirstName = FirstName;
            Assert.That(entity.DisplayName, Is.EqualTo(JohnSmithName));
        }

        private const string Name = "Acme Inc";
        private const string AltName = "Smith";
        private const string FirstName = "John";
        private const string AcmeName = Name;
        private const string JohnSmithName = "John Smith";
    }
}
