using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Hosting;
using System;
using FPTeLabLibarry.Extesions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.Xml;

namespace FPTeLabService
{
    public class GetDataOrderHis_v2 : HostedServiceOther
    {
        private ISettings settings;
        private BackgroundTaskQueueOther taskQueue;
        private ConnectioneLab _conneLab;
        private readonly ILogger logger;
        private FPT.eHospital.LISClient.LabConnection His_v2;

        public GetDataOrderHis_v2(ISettings _settings
            , BackgroundTaskQueueOther _taskQueue
            , ConnectioneLab conneLab
            , ILoggerFactory loggerFactory): base(loggerFactory)
        {
            settings = _settings;
            taskQueue = _taskQueue;
            _conneLab = conneLab;
            logger = loggerFactory.CreateLogger(typeof(GetDataOrderHis_v2));
            His_v2 = new FPT.eHospital.LISClient.LabConnection(settings.Token, "token", settings.UrlApi, settings.BenhVien_Id, settings.UserName);
        }

        protected override int Repeat { get { return settings.Repeat; } }
        protected override void OnExecuteTask(object state)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            //Get PatientType
            var patientType = His_v2.GetPatientTypeLab(settings.BenhVien_Id);
            paramList.Add("PatientType", patientType);
            //Get Employee
            var employee = His_v2.GetEmployeeLab(settings.BenhVien_Id);
            paramList.Add("Employee", employee);
            //Get Department
            var department = His_v2.GetDepartmenteLab(settings.BenhVien_Id);
            paramList.Add("Department", department);
            //Get Danh mục dịch vụ
            var dichvu = His_v2.GetDichVu();
            paramList.Add("Services", dichvu);
            //Get chỉ định dịch vụ
            var chiDinh = His_v2.GetPatientHasNotSID(settings.BenhVien_Id);
            paramList.Add("ChiDinhDichVu", chiDinh);
            InsertDataeLab_v2 dataELab = new InsertDataeLab_v2(settings, _conneLab, paramList, logger);
            taskQueue.QueueBackgroundWorkItem(token => dataELab.InsertData(token));
        }
    }
}
