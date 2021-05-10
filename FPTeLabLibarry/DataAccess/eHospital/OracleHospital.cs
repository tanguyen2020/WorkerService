using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FPTeLabLibarry.DataAccess
{
    public class OracleHospital: OracleBase
    {
        private Connection _oracle;
        public OracleHospital(Connection oracle, string connectString) : base(connectString)
        {
            _oracle = oracle;
        }
        public override Task<int> ExecuteNonQueryAsync(string cmdId, IDictionary<string, object> paramList)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(paramList);

            return _conn.ExecuteAsync(query, paramList);
        }
        public override Task<int> ExecuteNonQueryAsync(string cmdId, object paramList)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(paramList);

            return _conn.ExecuteAsync(query, paramList);
        }
        public override int ExecuteCommand(string cmdId, object paramList)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(paramList);
            return _conn.Execute(query, paramList);
        }
        public override DataTable ExecuteQuery(string cmdId, IDictionary<string, object> parameter)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);

            return Query(query, parameter, CommandType.Text);
        }
        public override DataTable ExecuteSPQuery(string cmdId, IDictionary<string, object> parameter)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);

            return Query(query, parameter, CommandType.StoredProcedure);
        }
        public override Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId, IDictionary<string, object> paramList)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);
            return ExecuteAsync<T>(query, paramList);
        }
        public override Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);
            return ExecuteAsync<T>(query, null);
        }
        public override Task<T> ExecuteTopAsync<T>(string cmdId, IDictionary<string, object> paramList)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);
            return ExecuteQueryFirstAsync<T>(query, paramList);
        }
        public override Task<T> ExecuteTopAsync<T>(string cmdId)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);
            return ExecuteQueryFirstAsync<T>(query, null);
        }
        public override Task<T> ExecuteTopAsync<T>(string cmdId, object parameter)
        {
            IStatement statement = _oracle.GetStatement(cmdId);
            string query = statement.GetCommandText(null);
            return ExecuteQueryFirstAsync<T>(query, parameter);
        }
    }
}
