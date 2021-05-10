using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace FPTeLabLibarry.DataAccess
{
    public abstract class BaseConnection : IExecuteConnection
    {
        private bool disposed = false;
        public DbConnection _conn;
        public BaseConnection(string ConnectString)
        {
            _conn = CreateConnection(ConnectString);
            _conn.Open();
        }
        protected abstract DbConnection CreateConnection(string connectionString);
        protected abstract DbDataAdapter CreateAdapter(DbCommand cmd);
        protected abstract void ParseParameters(DbCommand cmd, IDictionary<string, object> paramList);
        public int ExecuteNonQuery(string cmdId)
        {
            return ExecuteNonQuery(cmdId, null);
        }
        public int ExecuteNonQuery(string cmdId, IDictionary<string, object> parameter)
        {
            return ExecuteNonQueryAsync(cmdId, parameter).Result;
        }
        public int ExecuteNonQuery(string cmdId, object parameter)
        {
            return ExecuteNonQueryAsync(cmdId, parameter).Result;
        }
        public abstract int ExecuteCommand(string cmdId, object parameter);
        public DataTable ExecuteQuery(string cmdId)
        {
            return ExecuteQuery(cmdId, null);
        }
        public abstract Task<int> ExecuteNonQueryAsync(string cmdId, IDictionary<string, object> paramList);
        public abstract Task<int> ExecuteNonQueryAsync(string cmdId, object paramList);
        public abstract DataTable ExecuteQuery(string cmdId, IDictionary<string, object> parameter);
        public abstract DataTable ExecuteSPQuery(string cmdId, IDictionary<string, object> parameter);
        protected DataTable Query(string query, IDictionary<string, object> paramList, CommandType commandType)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = _conn.CreateCommand())
            using (DbDataAdapter sda = CreateAdapter(cmd))
            {
                cmd.CommandText = query;
                cmd.CommandType = commandType;
                if (paramList != null)
                {
                    ParseParameters(cmd, paramList);
                }
                int rows = sda.Fill(dt);
            }
            return dt;
        }
        public abstract Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId, IDictionary<string, object> paramList);
        public abstract Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId);
        public abstract Task<T> ExecuteTopAsync<T>(string cmdId);
        public abstract Task<T> ExecuteTopAsync<T>(string cmdId, IDictionary<string, object> paramList);
        public abstract Task<T> ExecuteTopAsync<T>(string cmdId, object parameter);
        public Task<IEnumerable<T>> ExecuteAsync<T>(string query, IDictionary<string, object> paramList)
        {
            return _conn.QueryAsync<T>(query, paramList);
        }
        public Task<T> ExecuteQueryFirstAsync<T>(string query, IDictionary<string, object> paramList)
        {
            return _conn.QuerySingleOrDefaultAsync<T>(query, paramList);
        }
        public Task<T> ExecuteQueryFirstAsync<T>(string query, object parameter)
        {
            return _conn.QuerySingleOrDefaultAsync<T>(query, parameter);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _conn.Close();
                _conn.Dispose();
            }

            disposed = true;
        }
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new BaseTransaction(_conn.BeginTransaction(isolationLevel));
        }
    }
}
