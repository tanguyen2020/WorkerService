using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class Result
    {
        public int CLSKetQua_Id { get; set; }
        public int TiepNhan_Id { get; set; }
        public int BenhAn_Id { get; set; }
        public string MaYTe { get; set; }
        public string TenBenhNhan { get; set; }
        public int NamSinh { get; set; }
        public int GioiTinh { get; set; }
        public DateTime ThoiGianThucHien { get; set; }
        public int NoiThucHien_Id { get; set; }
        public int BacSiThucHien_Id { get; set; }
        public int BacSiKetLuan_Id { get; set; }
        public string ChanDoanLamSang { get; set; }
        public string KetLuan { get; set; }
        public string GhiChu { get; set; }
        public int LuuTru_Id { get; set; }
        public int LuuTruChiTiet_Id { get; set; }
        public IEnumerable<ResultDetail> CLSKetQuaChiTiet { get; set; }
    }
}
