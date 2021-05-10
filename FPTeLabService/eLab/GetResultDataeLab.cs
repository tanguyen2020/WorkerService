using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FPTeLabService
{
    public class GetResultDataeLab : HostedServiceResult
    {
        private ISettings settings;
        private BackgroundTaskQueueResult taskQueue;
        private Connection conHis;
        private ConnectioneLab conneLab;
        private readonly ILogger logger;

        protected override int RepeatResult { get { return settings.RepeatResult; } }

        public GetResultDataeLab(ISettings _settings
            , BackgroundTaskQueueResult _taskQueue
            , Connection _conHis
            , ConnectioneLab _conneLab
            , ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            settings = _settings;
            taskQueue = _taskQueue;
            conHis = _conHis;
            conneLab = _conneLab;
            logger = loggerFactory.CreateLogger(typeof(GetResultDataeLab));
        }
        protected override void OnExecuteTaskResult(object state)
        {
            Dictionary<string, DataTable> paramList = new Dictionary<string, DataTable>();
            using (var _conn = conneLab.CreateConnection())
            {
                //Get data result from database eLab
                ReturnDataHis_v1 his_V1 = new ReturnDataHis_v1(settings, conHis, paramList);
                taskQueue.QueueBackgroundWorkItem(token => his_V1.ReturnDataHis(token));
            }
        }
    }
}
