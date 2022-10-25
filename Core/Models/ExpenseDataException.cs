using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmxLabs.Acct.Core.Models
{
    public class ExpenseDataException : DataQualityException
    {
        public ExpenseDataException(ulong ExpenseId_, string message_)
            : base($"Data quality error on expense id {ExpenseId_}: {message_}")
        {
            
        }   
    }
}
