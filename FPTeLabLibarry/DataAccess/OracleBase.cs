using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public abstract class OracleBase : BaseConnection
    {
        public OracleBase(string connectString) : base(connectString)
        {

        }
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }
        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new OracleDataAdapter(cmd as OracleCommand);
        }
        protected override void ParseParameters(DbCommand cmd, IDictionary<string, object> paramList)
        {
            OracleCommand oracleCommand = cmd as OracleCommand;
            foreach (var kvp in paramList)
            {
                if (kvp.Key == string.Empty) continue;
                oracleCommand.Parameters.Add("@" + kvp.Key, kvp.Value ?? DBNull.Value);
            }
        }
    }
}
