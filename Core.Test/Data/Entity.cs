using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Test.Data
{
    public static class Entity
    {
        public class Literal : IEntity
        {
            public const string EntityId = "EntityId";
            public const string Name = "Name";
            public const string FirstName = "First Name";
            public const string DoorNumber = "Door Number";
            public const string Building = "Building";
            public const string StreetNumber = "Street Number";
            public const string StreetName = "Street Name";
            public const string City = "City";
            public const string PostCode = "Post Code";
            public const string Country = "Country";
            public const string Website = "Website";
            public const string Phone = "Phone";
            public const bool IsCorp = true;
            public const string BillingEmail = "billing@email.com";
            public static Address AddressData = new Address();

            static  Literal()
            {
                AddressData.DoorNumber = DoorNumber;
                AddressData.Building = Building;
                AddressData.StreetNumber = StreetNumber;
                AddressData.StreetName = StreetName;
                AddressData.City = City;
                AddressData.PostCode = PostCode;
                AddressData.Country = Country;
            }

            public static IEntity Instance { get; } = new Literal();

            string IEntity.Id => EntityId;

            string IEntity.Name => Name;

            string IEntity.FirstName => FirstName;

            string IEntity.DisplayName => $"{FirstName} {Name}";

            public IAddress Address => AddressData;

            string IEntity.Website => Website;

            string IEntity.Phone => Phone;

            bool IEntity.IsCorp => IsCorp;

            string IEntity.BillingEmail => BillingEmail;
        }

        public class Acme : IEntity
        {
            public const string EntityId = "Acme";
            public const string Name = "Acme Inc";
            public const string FirstName = null;
            public const string DoorNumber = "1";
            public const string Building = "Acme House";
            public const string StreetNumber = null;
            public const string StreetName = "Acme Way";
            public const string City = "California";
            public const string PostCode = "CA1 1AA";
            public const string Country = "United Europe";
            public const string Website = "www.acme.com";
            public const string Phone = "123456789";
            public const bool IsCorp = true;
            public const string BillingEmail = "accounts@acme.com";
            public static Address AddressData = new Address();

            static Acme()
            {
                AddressData.DoorNumber = DoorNumber;
                AddressData.Building = Building;
                AddressData.StreetNumber = StreetNumber;
                AddressData.StreetName = StreetName;
                AddressData.City = City;
                AddressData.PostCode = PostCode;
                AddressData.Country = Country;
            }

            public static IEntity Instance { get; } = new Acme();

            public string Id => EntityId;

            string IEntity.Name => Name;

            string IEntity.FirstName => FirstName;

            string IEntity.DisplayName => Name;

            public IAddress Address => AddressData;

            string IEntity.Website => Website;

            string IEntity.Phone => Phone;

            bool IEntity.IsCorp => IsCorp;

            string IEntity.BillingEmail => BillingEmail;
        }

        public class NoBill : IEntity
        {
            public const string EntityId = "NoBill";
            public const string Name = "No";
            public const string FirstName = "Billing";
            public const string DoorNumber = null;
            public const string Building = null;
            public const string StreetNumber = null;
            public const string StreetName = null;
            public const string City = "London";
            public const string PostCode = "NW1 1AA";
            public const string Country = "UK";
            public const string Website = null;
            public const string Phone = null;
            public const bool IsCorp = true;
            public const string BillingEmail = null;
            public static Address AddressData = new Address();

            static NoBill()
            {
                AddressData.DoorNumber = DoorNumber;
                AddressData.Building = Building;
                AddressData.StreetNumber = StreetNumber;
                AddressData.StreetName = StreetName;
                AddressData.City = City;
                AddressData.PostCode = PostCode;
                AddressData.Country = Country;
            }

            public static IEntity Instance { get; } = new NoBill();

            public string Id => EntityId;

            string IEntity.Name => Name;

            string IEntity.FirstName => FirstName;

            string IEntity.DisplayName => Name;

            public IAddress Address => AddressData;

            string IEntity.Website => Website;

            string IEntity.Phone => Phone;

            bool IEntity.IsCorp => IsCorp;

            string IEntity.BillingEmail => BillingEmail;
        }
    }
}