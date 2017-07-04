using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public interface IAddress
    {
        string DoorNumber { get; }
        string Building { get; }
        string StreetNumber { get; }
        string StreetName { get; }
        string City { get; }
        string PostCode { get; }
        string Country { get; }
    }
}
