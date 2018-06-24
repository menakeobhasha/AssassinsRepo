using System.Data;

namespace Connection
{
    public class Helper
    {
        public static T GetDataValue<T>(IDataReader dr, string columnName)
        {
            int ordinal = dr.GetOrdinal(columnName);
            if (!dr.IsDBNull(ordinal))
                return (T)dr.GetValue(ordinal);
            return default(T);
        }
    }
}
