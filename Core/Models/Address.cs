namespace HmxLabs.Acct.Core.Models
{
    public class Address : IAddress
    {
        public string DoorNumber { get; set; }
        public string Building { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
