using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace FPTeLabService
{
    public class InsertDataeLab_v2
    {
        private ISettings settings;
        private ConnectioneLab _conneLab;
        private IDictionary<string, object> paramList;
        private FPT.eHospital.LISClient.LabConnection His_v2;
        private readonly ILogger _logger;
        public InsertDataeLab_v2(ISettings _settings
            , ConnectioneLab conneLab
            , IDictionary<string, object> _paramList
            , ILogger logger)
        {
            settings = _settings;
            _conneLab = conneLab;
            paramList = _paramList;
            His_v2 = new FPT.eHospital.LISClient.LabConnection(settings.Token, "token", settings.UrlApi, settings.BenhVien_Id, settings.UserName);
            _logger = logger;
        }
        public void InsertData(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;
            try
            {
                IExecuteDataSource executeData = new ExecuteDataSource(settings, _conneLab, _logger);
                using (var _conn = _conneLab.CreateConnection())
                {
                    Stopwatch watch = new Stopwatch();
                    if (paramList.Count > 0)
                    {
                        #region Get PatientType
                        var PatientTypes = paramList["PatientType"] as List<FPT.eHospital.LISClient.PatientTypeLab>;
                        List<PatientTypeInfo> listPatientType = new List<PatientTypeInfo>();
                        if (PatientTypes.Count > 0)
                        {
                            foreach (var p in PatientTypes)
                            {
                                PatientTypeInfo pTypeIU = new PatientTypeInfo();
                                pTypeIU.PatientType_Id = p.DOITUONG_ID;
                                pTypeIU.PatientTypeCode = p.MADOITUONG;
                                pTypeIU.PatientTypeName = p.TENDOITUONG;
                                pTypeIU.Enabled = p.TAMNGUNG;
                                listPatientType.Add(pTypeIU);
                            }
                            executeData.ExecutePatientType(listPatientType);
                        }
                        #endregion

                        #region Get List Department
                        var Departments = paramList["Department"] as List<FPT.eHospital.LISClient.DepartmenteLab>;
                        List<DepartmentInfo> listDepartment = new List<DepartmentInfo>();
                        if (Departments.Count > 0)
                        {
                            foreach (var item in Departments)
                            {
                                DepartmentInfo dep = new DepartmentInfo();
                                dep.DepartmentID = item.PHONGBAN_ID;
                                dep.DepartmentCode = item.MAPHONGBAN;
                                dep.DepartmentName = item.TENPHONGBAN;
                                dep.Level = item.CAP;
                                dep.ParentID = item.CAPTREN_ID;
                                dep.IsMadeServices = item.COTHUCHIENDICHVU;
                                dep.Enabled = item.TAMNGUNG;
                                listDepartment.Add(dep);
                            }
                            executeData.ExecuteDepartment(listDepartment);
                        }
                        #endregion

                        #region Get List Employee
                        var Employees = paramList["Employee"] as List<FPT.eHospital.LISClient.EmployeeLab>;
                        List<EmployeeInfo> listEmployee = new List<EmployeeInfo>();
                        if (Employees.Count > 0)
                        {
                            foreach (var emp in Employees)
                            {
                                EmployeeInfo employee = new EmployeeInfo();
                                employee.Employee_ID = emp.NHANVIEN_ID;
                                employee.EmployeeCode = emp.MANHANVIEN;
                                employee.EmployeeName = emp.TENNHANVIEN;
                                employee.DepartmentID = emp.PHONGBAN_ID;
                                employee.Nationality = emp.QUOCGIA;
                                employee.Enabled = emp.TAMNGUNG;
                                employee.Address = emp.DIACHI;
                                if (emp.GIOITINH == 1)
                                    employee.Sex = "Nam";
                                else
                                    employee.Sex = "Nữ";
                                listEmployee.Add(employee);
                            }
                            executeData.ExecuteEmployee(listEmployee);
                        }
                        #endregion

                        #region Get Mapping Service
                        var Services = paramList["Services"] as List<FPT.eHospital.LISClient.Model.DichVuModel>;
                        List<MappingServices> mappingServices = new List<MappingServices>();
                        if (Services.Count > 0)
                        {
                            foreach (var item in Services)
                            {
                                MappingServices mapp = new MappingServices();
                                mapp.ServiceId_His = item.DichVu_Id;
                                mapp.ServiceCode_His = item.MaDichVu;
                                mapp.ServiceName_His = item.TenDichVu;
                                mapp.CreateDate = DateTime.Now;
                                mapp.ModifyDate = DateTime.Now;
                                mappingServices.Add(mapp);
                            }
                            executeData.ExecuteServices(mappingServices);
                        }
                        #endregion

                        #region Get Chỉ định dịch vụ
                        var chiDinh = paramList["ChiDinhDichVu"] as List<FPT.eHospital.LISClient.PatientHasNotSID>;
                        List<PatientHasNotSID> patientHasNots = new List<PatientHasNotSID>();
                        if(chiDinh.Count > 0)
                        {
                            foreach(var item in chiDinh)
                            {
                                PatientHasNotSID patientHas = new PatientHasNotSID();
                                patientHas.DVYeuCau_His_Id = item.DVYEUCAU_ID;
                                patientHas.SoPhieuYeuCau = item.SOPHIEUYEUCAU;
                                patientHas.BacSiChiDinh_Id = item.BACSICHIDINH_ID;
                                patientHas.NoiThucHien_Id = item.NOITHUCHIEN_ID;
                                patientHas.BenhNhan_Id = item.BENHNHAN_ID;
                                patientHas.MaYTe = item.MAYTE;
                                patientHas.Ho = item.HO;
                                patientHas.Ten = item.TEN;
                                patientHas.TenBenhNhan = item.TENBENHNHAN;
                                patientHas.GioiTinh = item.GIOITINH;
                                patientHas.NamSinh = item.NAMSINH;
                                patientHas.NgaySinh = item.NGAYSINH;
                                patientHas.DiaChiLienLac = item.DIACHILIENLAC;
                                patientHas.NgayYeuCau = item.NGAYYEUCAU;
                                patientHas.ThoiGianYeuCau = item.NGAYGIOYEUCAU;
                                patientHas.DichVu_Id = item.DICHVU_ID;
                                patientHas.NgayTao = item.NGAYTAO;
                                patientHas.NguoiTao_Id = item.NGUOITAO_ID;
                                patientHasNots.Add(patientHas);
                            }
                            executeData.ExecutePatientAndService(patientHasNots);
                        }
                        #endregion
                    }
                }
            }
            catch(Exception ex)
            {
                ConsoleWriter.WriteMessage(ex.Message, ConsoleColor.Red);
                _logger.LogError(ex.Message);
            }
            finally { }
        }
    }
}
