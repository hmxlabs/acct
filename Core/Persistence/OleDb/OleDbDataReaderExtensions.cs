using System;
using System.Data.OleDb;

namespace HmxLabs.Acct.Core.Persistence.OleDb
{
    public static class OleDbDataReaderExtensions
    {
        public static string GetStringOrNull(this OleDbDataReader reader_, int ordinal_)
        {
            if (null == reader_)   
                throw new ArgumentNullException(nameof(reader_));

            if (reader_.IsDBNull(ordinal_))
                return null;

            return reader_.GetString(ordinal_);
        }

        public static DateTime? GetNullableDateTime(this OleDbDataReader reader_, int ordinal_)
        {
            if (null == reader_)
                throw new ArgumentNullException(nameof(reader_));

            if (reader_.IsDBNull(ordinal_))
                return null;

            return reader_.GetDateTime(ordinal_);
        }
    }
}
