using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System;
using Microsoft.Extensions.Logging;

namespace FPTeLabService
{
    public class InsertDataeLab_v1
    {
        private ISettings _settings;
        private ConnectioneLab _conneLab;
        private IDictionary<string, object> _paramList;
        private readonly ILogger _logger;
        public InsertDataeLab_v1(ISettings settings
            , ConnectioneLab conneLab
            , IDictionary<string, object> paramList
            , ILogger logger)
        {
            _settings = settings;
            _conneLab = conneLab;
            _paramList = paramList;
            _logger = logger;
        }
        public void InsertData(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;
            try
            {
                IExecuteDataSource executeData = new ExecuteDataSource(_settings, _conneLab, _logger);
                using (var _conn = _conneLab.CreateConnection())
                {
                    if (_paramList.Count > 0)
                    {
                        var PatientTypes = _paramList["PatientTypes"] as List<PatientTypeInfo>;
                        if (PatientTypes.Count > 0)
                            executeData.ExecutePatientType(PatientTypes);

                        var Departments = _paramList["Departments"] as List<DepartmentInfo>;
                        if (Departments.Count > 0)
                            executeData.ExecuteDepartment(Departments);

                        var Employees = _paramList["Employees"] as List<EmployeeInfo>;
                        List<EmployeeInfo> listEmployee = new List<EmployeeInfo>();
                        if (Employees.Count > 0)
                        {
                            foreach (var item in Employees)
                            {
                                if (item.Sex == "F")
                                    item.Sex = "Nữ";
                                else
                                    item.Sex = "Nam";
                                listEmployee.Add(item);
                            }
                            executeData.ExecuteEmployee(listEmployee);
                        }

                        var MappingServices = _paramList["MappingServices"] as List<MappingServices>;
                        if (MappingServices.Count > 0)
                            executeData.ExecuteServices(MappingServices);

                        var PatientHasNots = _paramList["PatientHasNotSID"] as List<PatientHasNotSID>;
                        if (PatientHasNots.Count > 0)
                            executeData.ExecutePatientAndService(PatientHasNots);
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleWriter.WriteMessage(ex.Message, ConsoleColor.Red);
                _logger.LogError(ex.Message);
            }
            finally { }
        }
    }
}
