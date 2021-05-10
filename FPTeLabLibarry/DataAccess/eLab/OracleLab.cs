using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FPTeLabLibarry.DataAccess
{
    public class OracleLab: OracleHospital
    {
        private ConnectioneLab _oracle;
        public OracleLab(ConnectioneLab oracle, string connectString) : base(oracle, connectString)
        {
            _oracle = oracle;
        }
    }
}
