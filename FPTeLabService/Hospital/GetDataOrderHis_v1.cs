using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Extesions;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FPTeLabService
{
    public class GetDataOrderHis_v1 : HostedServiceOther
    {
        private ISettings settings;
        private BackgroundTaskQueueOther taskQueue;
        private Connection connHis;
        private ConnectioneLab conneLab;
        private ILogger _logger;

        protected override int Repeat { get { return settings.Repeat; } }

        public GetDataOrderHis_v1(ISettings _settings
            , BackgroundTaskQueueOther _taskQueue
            , Connection _connHis
            , ConnectioneLab _conneLab
            , ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            settings = _settings;
            taskQueue = _taskQueue;
            connHis = _connHis;
            conneLab = _conneLab;
            _logger = loggerFactory.CreateLogger(typeof(GetDataOrderHis_v1));
        }
        protected override void OnExecuteTask(object state)
        {
            Dictionary<string, object> _paramList = new Dictionary<string, object>();
            using (var _conn = connHis.CreateConnection())
            {
                //Get PatientType
                IEnumerable<PatientTypeInfo> patientTypes = _conn.ExecuteQueryAsync<PatientTypeInfo>("GetPatientTypes").Result;
                _paramList.Add("PatientTypes", patientTypes);

                //GetDepartment
                IEnumerable<EmployeeInfo> employees = _conn.ExecuteQueryAsync<EmployeeInfo>("GetEmployees").Result;
                _paramList.Add("Employees", employees);

                //Get Employees
                IEnumerable<DepartmentInfo> departments = _conn.ExecuteQueryAsync<DepartmentInfo>("GetDepartments").Result;
                _paramList.Add("Departments", departments);

                //Get Services
                IEnumerable<MappingServices> mappingServices = _conn.ExecuteQueryAsync<MappingServices>("GetServices").Result;
                _paramList.Add("MappingServices", mappingServices);

                //Get Infomation services of patient
                IEnumerable<PatientHasNotSID> patientHasNots = _conn.ExecuteQueryAsync<PatientHasNotSID>("GetPatientHasNotSID").Result;
                _paramList.Add("PatientHasNotSID", patientHasNots);

                InsertDataeLab_v1 dataELab = new InsertDataeLab_v1(settings, conneLab, _paramList, _logger);
                taskQueue.QueueBackgroundWorkItem(token => dataELab.InsertData(token));
            }
        }
    }
}
