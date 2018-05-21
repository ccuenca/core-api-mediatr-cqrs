using System;
using System.Data;
using System.Data.SqlClient;

namespace TestMediaTR.Persistence
{
    public static class Parameters
    {
        public static SqlParameter CreateParameter(
            string name,
            object value,
            ParameterDirection direction,
            SqlDbType type)
        {

            return new SqlParameter
            {
                ParameterName = name,
                SqlDbType = type,
                Direction = direction,
                Value = value
            };
        }

        public static SqlParameter CreateOutputParameter(
            string name,
            SqlDbType type)
        {
            return new SqlParameter
            {
                ParameterName = name,
                SqlDbType = type,
                Direction = ParameterDirection.Output
            };
        }
    }
}
