using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FPTeLabService
{
    public class ReturnDataHis_v1
    {
        private ISettings settings;
        private Connection connHis;
        private IDictionary<string, DataTable> paramList;
        public ReturnDataHis_v1(ISettings _settings
            , Connection _connHis
            , IDictionary<string, DataTable> _paramList)
        {
            settings = _settings;
            connHis = _connHis;
            paramList = _paramList;
        }

        public void ReturnDataHis(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;
            try
            {
                using (var _conn = connHis.CreateConnection())
                {
                    //Do something........
                }
            }
            catch
            {

            }
        }
    }
}
