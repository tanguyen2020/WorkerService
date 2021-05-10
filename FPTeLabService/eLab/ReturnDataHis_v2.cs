using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using FPTeLabLibarry.Extesions;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace FPTeLabService
{
    public class ReturnDataHis_v2: HostedServiceOther
    {
        private ISettings settings;
        private BackgroundTaskQueueResult taskQueue;
        private Connection _connHis;
        private readonly ILogger logger;
        private FPT.eHospital.LISClient.LabConnection His_v2;
        public ReturnDataHis_v2(ISettings _settings
            , BackgroundTaskQueueResult _taskQueue
            , Connection connHis
            , ILoggerFactory loggerFactory): base(loggerFactory)
        {
            settings = _settings;
            taskQueue = _taskQueue;
            _connHis = connHis;
            logger = loggerFactory.CreateLogger(typeof(ReturnDataHis_v2));
            His_v2 = new FPT.eHospital.LISClient.LabConnection(settings.Token, "token", settings.UrlApi, settings.BenhVien_Id, settings.UserName);
        }

        protected override int Repeat { get { return settings.RepeatResult; } }
        protected override void OnExecuteTask(object state)
        {
            using (var _con = _connHis.CreateConnection())
            {
                //Get result data eLab to His
                //.........

                //Call api the get data from his
            }
        }
    }
}
