using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FPTeLabLibarry.DataAccess
{
    public class SqleLab: SqleHospital
    {
        private ConnectioneLab _sql;
        public SqleLab(ConnectioneLab sql, string connectString) : base(sql, connectString)
        {
            _sql= sql;
        }
    }
}
