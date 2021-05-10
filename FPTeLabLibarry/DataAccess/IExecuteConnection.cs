using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FPTeLabLibarry.DataAccess
{
    public interface IExecuteConnection : IDisposable
    {
        int ExecuteNonQuery(string cmdId);
        int ExecuteNonQuery(string cmdId, object parameter);
        int ExecuteNonQuery(string cmdId, IDictionary<string, object> parameter);
        int ExecuteCommand(string cmdId, object parameter);
        DataTable ExecuteSPQuery(string cmdId, IDictionary<string, object> parameter);
        DataTable ExecuteQuery(string cmdId);
        DataTable ExecuteQuery(string cmdId, IDictionary<string, object> parameter);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string cmdId, IDictionary<string, object> paramList);
        Task<T> ExecuteTopAsync<T>(string cmdId);
        Task<T> ExecuteTopAsync<T>(string cmdId, IDictionary<string, object> paramList);
        Task<T> ExecuteTopAsync<T>(string cmdId, object parameter);
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
