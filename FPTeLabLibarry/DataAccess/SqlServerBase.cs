using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FPTeLabLibarry.DataAccess
{
    public abstract class SqlServerBase : BaseConnection
    {
        public SqlServerBase(string connectString) : base(connectString)
        {

        }

        protected override DbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new SqlDataAdapter(cmd as SqlCommand);
        }
        protected override void ParseParameters(DbCommand cmd, IDictionary<string, object> paramList)
        {
            SqlCommand sqlCommand = cmd as SqlCommand;
            foreach (var kvp in paramList)
            {
                if (kvp.Key == string.Empty) continue;
                sqlCommand.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
            }
        }
    }
}
