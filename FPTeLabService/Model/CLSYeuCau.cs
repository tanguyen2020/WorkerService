using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class CLSYeuCau
    {
        public int? CLSYeuCau_Id { get; set; }
        public string SoPhieuYeuCau { get; set; }
        public DateTime NgayYeuCau { get; set; }
        public DateTime ThoiGianYeuCau { get; set; }
        public int? BacSiChiDinh_Id { get; set; }
        public int? NoiThucHien_Id { get; set; }
        public int? BenhNhan_Id { get; set; }
        public int DVYeuCau_Id_His { get; set; }
        public int? DichVu_Id { get; set; }
    }
    public class CLSYeuCauChiTiet
    {
        public int? YeuCauChiTiet_Id { get; set; }
        public int? CLSYeuCau_Id { get; set; }
        public int? DichVu_Id { get; set; }
    }
}
