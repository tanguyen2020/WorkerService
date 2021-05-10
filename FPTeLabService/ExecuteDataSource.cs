using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FPTeLabService
{
    public interface IExecuteDataSource
    {
        void ExecuteServices(List<MappingServices> listServices);
        void ExecuteEmployee(List<EmployeeInfo> listEmployees);
        void ExecuteDepartment(List<DepartmentInfo> listDepartments);
        void ExecutePatientType(List<PatientTypeInfo> listPatientTypes);
        void ExecutePatientAndService(List<PatientHasNotSID> listPatients);
    }
    internal class UpperCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToUpper();
        }

        protected override string ResolveDictionaryKey(string dictionaryKey)
        {
            return dictionaryKey.ToUpper();
        }

    }
    public class ExecuteDataSource : IExecuteDataSource
    {
        private ISettings settings;
        private ConnectioneLab _connection;
        private readonly ILogger _logger;
        Stopwatch watch = new Stopwatch();
        private JsonSerializerSettings Jsonsettings = new JsonSerializerSettings()
        {
            //ContractResolver = new UpperCaseContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateFormatString = "yyyy-MM-ddTHH:mm:ss"
        };
        public ExecuteDataSource(ISettings _settings
            , ConnectioneLab connection
            , ILogger logger)
        {
            settings = _settings;
            _connection = connection;
            _logger = logger;
        }

        public void ExecuteServices(List<MappingServices> listServices)
        {
            using (var _conn = _connection.CreateConnection())
            {
                List<MappingServices> listMapIn = new List<MappingServices>();
                List<MappingServices> listMapUp = new List<MappingServices>();
                IEnumerable<MappingServices> services = _conn.ExecuteQueryAsync<MappingServices>("GetMappingService").Result;
                foreach (var item in listServices)
                {
                    string itemCode = item.ServiceCode_His.Normalize();
                    MappingServices svc = services.FirstOrDefault(x => x.ServiceCode_His.Normalize() == itemCode);
                    if (svc != null)
                        listMapUp.Add(item);
                    else
                        listMapIn.Add(item);
                }
                if (listMapIn.Count > 0)
                {
                    watch.Start();
                    int empIn = _conn.ExecuteNonQuery("InsertDichVuMappingJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listMapIn, Jsonsettings) });
                    watch.Stop();
                    this.Writer2("Insert Service", empIn, listMapIn.Count, watch);
                }
                if (listMapUp.Count > 0)
                {
                    watch.Start();
                    int empUp = _conn.ExecuteNonQuery("UpdateDichVuMappingJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listMapUp, Jsonsettings) });
                    watch.Stop();
                    this.Writer2("Update Service", empUp, listMapUp.Count, watch);
                }
            }
        }
        public void ExecuteEmployee(List<EmployeeInfo> listEmployees)
        {
            using (var _conn = _connection.CreateConnection())
            {
                List<EmployeeInfo> listEmpIn = new List<EmployeeInfo>();
                List<EmployeeInfo> listEmpUpdate = new List<EmployeeInfo>();
                IEnumerable<EmployeeInfo> lstEmp = _conn.ExecuteQueryAsync<EmployeeInfo>("GetEmployee").Result;
                foreach (var emp in listEmployees)
                {
                    if (lstEmp.Where(x => x.EmployeeCode == emp.EmployeeCode).ToList().Count > 0)
                        listEmpUpdate.Add(emp);
                    else
                        listEmpIn.Add(emp);
                }
                if (listEmpIn.Count > 0)
                {
                    watch.Start();
                    int empIn = _conn.ExecuteNonQuery("InsertEmployeeJson"
                        , new { DATA = JsonConvert.SerializeObject(listEmpIn) });
                    watch.Stop();
                    this.Writer2("Insert Employee", empIn, listEmpIn.Count, watch);
                }
                if (listEmpUpdate.Count > 0)
                {
                    watch.Start();
                    int empUp = _conn.ExecuteNonQuery("UpdateEmployeeJson"
                        , new { DATA = JsonConvert.SerializeObject(listEmpUpdate) });
                    watch.Stop();
                    this.Writer2("Update Employee", empUp, listEmpUpdate.Count, watch);
                }
            }
        }
        public void ExecuteDepartment(List<DepartmentInfo> listDepartments)
        {
            using (var _conn = _connection.CreateConnection())
            {
                List<DepartmentInfo> listDepInsert = new List<DepartmentInfo>();
                List<DepartmentInfo> listDepUpdate = new List<DepartmentInfo>();
                IEnumerable<DepartmentInfo> lstDep = _conn.ExecuteQueryAsync<DepartmentInfo>("GetDepartment").Result;
                foreach (var dep in listDepartments)
                {
                    if (lstDep.Where(x => x.DepartmentCode == dep.DepartmentCode).ToList().Count > 0)
                        listDepUpdate.Add(dep);
                    else
                        listDepInsert.Add(dep);
                }
                if (listDepInsert.Count > 0)
                {
                    watch.Start();
                    int depIn = _conn.ExecuteNonQuery("InsertDepartmentJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listDepInsert) });
                    this.Writer2("Insert Department", depIn, listDepInsert.Count, watch);
                    watch.Stop();
                }
                if (listDepUpdate.Count > 0)
                {
                    watch.Start();
                    int depUpdate = _conn.ExecuteNonQuery("UpdateDepartmentJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listDepUpdate) });
                    this.Writer2("Update Department", depUpdate, listDepUpdate.Count, watch);
                    watch.Stop();
                }
            }
        }
        public void ExecutePatientType(List<PatientTypeInfo> listPatientTypes)
        {
            Stopwatch watch = new Stopwatch();
            using (var _conn = _connection.CreateConnection())
            {
                List<PatientTypeInfo> listPTypeIn = new List<PatientTypeInfo>();
                List<PatientTypeInfo> listPTypeUpdate = new List<PatientTypeInfo>();
                IEnumerable<PatientTypeInfo> lstPType = _conn.ExecuteQueryAsync<PatientTypeInfo>("GetPatientType").Result;
                foreach (var item in listPatientTypes)
                {
                    if (lstPType.Where(x => x.PatientTypeCode == item.PatientTypeCode).ToList().Count > 0)
                        listPTypeUpdate.Add(item);
                    else
                        listPTypeIn.Add(item);
                }
                if (listPTypeIn.Count > 0)
                {
                    watch.Start();
                    int PType = _conn.ExecuteNonQuery("InsertPatientTypeJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listPTypeIn) });
                    watch.Stop();
                    this.Writer2("Insert PatientType", PType, listPTypeIn.Count, watch);
                }
                if (listPTypeUpdate.Count > 0)
                {
                    watch.Start();
                    int PType = _conn.ExecuteNonQuery("UpdatePatientTypeJson"
                        , new { JSON_DATA = JsonConvert.SerializeObject(listPTypeUpdate) });
                    watch.Stop();
                    this.Writer2("Update PatientType", PType, listPTypeUpdate.Count, watch);
                }
            }
        }
        public void ExecutePatientAndService(List<PatientHasNotSID> listPatients)
        {
            using (var _conn = _connection.CreateConnection())
            {
                ITransaction transaction = _conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var item in listPatients)
                    {
                        watch.Start();
                        PatientInfo p = new PatientInfo();
                        p.PatientID = 0;
                        p.HospitalCode = settings.BenhVien_Id;
                        p.PatientCode = item.MaYTe;
                        p.IssueDate = item.NgayTao;
                        p.PatientName = item.TenBenhNhan;
                        p.FirstName = item.Ho;
                        p.LastName = item.Ten;
                        p.DOB = item.NgaySinh;
                        p.YOB = item.NamSinh;
                        if (item.GioiTinh == 1)
                            p.Sex = "T";
                        else
                            p.Sex = "G";
                        p.PatientID_His = item.BenhNhan_Id;
                        p.Address = item.DiaChiLienLac;
                        p.CreatedDate = item.NgayTao;
                        p.ModifiedDate = item.NgayTao;
                        p.CreatedBy = item.NguoiTao_Id;
                        p.ModifiedBy = item.NguoiTao_Id;
                        var param = new Dictionary<string, object>() { { "PatientCode", item.MaYTe } };
                        var patient = _conn.ExecuteTopAsync<PatientInfo>("GetPatientWithCode", param).Result;
                        int? pInfo = 0;
                        if (patient == null)
                        {
                            watch.Start();
                            pInfo = _conn.ExecuteCommand("InsertPatient", p);
                            watch.Stop();
                            this.Writer("Insert Patient", item.MaYTe, pInfo, watch);
                        }
                        else
                        {
                            watch.Start();
                            int updatePatient = _conn.ExecuteNonQuery("UpdatePatient", p);
                            watch.Stop();
                            this.Writer("Update Patient", item.MaYTe, updatePatient, watch);
                            pInfo = patient.PatientID;
                        }
                        //Insert Chỉ định dịch vụ chưa lấy mẫu
                        CLSYeuCau canLamSang = new CLSYeuCau();
                        canLamSang.DVYeuCau_Id_His = item.DVYeuCau_His_Id;
                        canLamSang.NgayYeuCau = item.NgayYeuCau;
                        canLamSang.ThoiGianYeuCau = item.ThoiGianYeuCau;
                        canLamSang.SoPhieuYeuCau = item.SoPhieuYeuCau;
                        canLamSang.BacSiChiDinh_Id = item.BacSiChiDinh_Id;
                        canLamSang.NoiThucHien_Id = item.NoiThucHien_Id;
                        canLamSang.BenhNhan_Id = pInfo;
                        var param2 = new Dictionary<string, object>() { { "DVYeuCau_Id_His", item.DVYeuCau_His_Id } };
                        var yeucau = _conn.ExecuteTopAsync<CLSYeuCau>("GetSubclinical", param2).Result;
                        int? yeucau_Id = 0;
                        if (yeucau == null)
                        {
                            watch.Start();
                            yeucau_Id = _conn.ExecuteCommand("InsertCLSYeuCau", canLamSang);
                            watch.Stop();
                            this.Writer("Insert Subclinical", item.SoPhieuYeuCau, yeucau_Id, watch);
                        }
                        else
                        {
                            watch.Start();
                            int update = _conn.ExecuteNonQuery("UpdateCLSYeuCau", canLamSang);
                            watch.Stop();
                            this.Writer("Update Subclinical", item.SoPhieuYeuCau, update, watch);
                            yeucau_Id = yeucau.CLSYeuCau_Id;
                        }
                        transaction.Commit();
                    }
                }
                catch(Exception ex)
                {
                    transaction.RollBack();
                    ConsoleWriter.WriteMessage(ex.Message, ConsoleColor.Red);
                    _logger.LogError(ex.Message);
                }
            }
        }
        private void Writer(string messages, string code, int? result, Stopwatch watch)
        {
            if (result > 0)
                ConsoleWriter.WriteMessage($"{messages} '{code}' => Success (Total time: '{watch.ElapsedMilliseconds}' ms)", ConsoleColor.Green);
            else
            {
                ConsoleWriter.WriteMessage($"{messages} '{code}' => Failed", ConsoleColor.Red);
                _logger.LogError($"{messages} '{code}' => Failed.");
            }
        }
        private void Writer2(string messages, int? result, int count, Stopwatch watch)
        {
            if (result > 0)
                ConsoleWriter.WriteMessage($"{messages} (Total Record: {count})  => Success (Total time: '{watch.ElapsedMilliseconds}' ms)", ConsoleColor.Green);
            else
            {
                ConsoleWriter.WriteMessage($"{messages} => Failed", ConsoleColor.Red);
                _logger.LogError($"{messages} => Failed.");
            }
        }
    }
}
