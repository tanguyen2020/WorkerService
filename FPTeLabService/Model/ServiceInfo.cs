using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class ServiceInfo
    {
        public int? DichVu_Id { get; set; }
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string MaDichVu_Seg01 { get; set; }
        public string MaDichVu_Seg02 { get; set; }
        public string MaDichVu_Seg03 { get; set; }
        public string MaDichVu_Seg04 { get; set; }
        public int? Cap { get; set; }
        public int? CapTren_Id { get; set; }
        public string MaQuiDinh { get; set; }
        public string InputCode { get; set; }
        public string ShortName { get; set; }
        public string DonViTinh { get; set; }
        public int? STT { get; set; }
        public bool? Test { get; set; }
        public bool? TamNgung { get; set; }
        public bool ChonHetCapDuoi { get; set; }
        public int? ApplyFor { get; set; }
        public bool? ThucHienBenNgoai { get; set; }
        public bool? CoGiaTriChuan { get; set; }
        public bool? CoGiaDichVu { get; set; }
        public bool? GiaCoDinh { get; set; }
        public bool? NoResult { get; set; }
        public int? LoaiDichVu_Id { get; set; }
    }
}
