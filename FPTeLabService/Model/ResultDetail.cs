using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class ResultDetail
    {
        public int DVYeuCau_Id { get; set; }
        public int DichVu_Id { get; set; }
        public string DonViTinh { get; set; }
        public string KetQua { get; set; }
        public string MucBinhThuong { get; set; }
        public bool IsBatThuong { get; set; }
        public bool IsGuiNgoai { get; set; }
        public string KetQuaCay { get; set; }
        public string KetQuaSoi { get; set; }
    }
}
