using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class PatientHasNotSID
    {
        public string SoPhieuYeuCau { get; set; }
        public int? BacSiChiDinh_Id { get; set; }
        public int? NoiThucHien_Id { get; set; }
        public DateTime ThoiGianYeuCau { get; set; }
        public DateTime NgayYeuCau { get; set; }
        public int? NguoiTao_Id { get; set; }
        public DateTime NgayTao { get; set; }
        public int DVYeuCau_His_Id { get; set; }
        public string DiaChiLienLac { get; set; }
        public int NamSinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string TenBenhNhan { get; set; }
        public string Ten { get; set; }
        public string Ho { get; set; }
        public string MaYTe { get; set; }
        public int BenhNhan_Id { get; set; }
        public int GioiTinh { get; set; }
        public int DichVu_Id { get; set; }
    }
}
