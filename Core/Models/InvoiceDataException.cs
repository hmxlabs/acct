namespace HmxLabs.Acct.Core.Models
{
    public class InvoiceDataException : DataQualityException
    {
        public InvoiceDataException(ulong invoiceId_, string message_)
            : base($"Data quality error on invoice id {invoiceId_}: {message_}")
        {
        }
    }
}
