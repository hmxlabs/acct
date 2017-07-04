using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public class InvoiceItemDataException : DataQualityException
    {
        public InvoiceItemDataException(ulong id_, string message_) :
            base($"Data quality error on invoice item id {id_} : {message_}")
        {
        }
    }
}
