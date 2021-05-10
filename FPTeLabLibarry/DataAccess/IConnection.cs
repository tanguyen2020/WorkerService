using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public interface IConnection : IDisposable
    {
        IExecuteConnection CreateConnection();
        IStatement GetStatement(string id);
    }
}
