using System;

namespace HmxLabs.Acct.Core.Models
{
    public class DataQualityException : Exception
    {
        public DataQualityException(string message_) : base(message_)
        {
        }

        public DataQualityException(string message_, Exception innerException_) : base(message_, innerException_)
        {
        }
    }
}
