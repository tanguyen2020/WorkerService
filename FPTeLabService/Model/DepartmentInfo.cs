using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class DepartmentInfo
    {
        public int DepartmentID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool Enabled { get; set; }
        public int? Level { get; set; }
        public int? ParentID { get; set; }
        public bool IsMadeServices { get; set; }
    }
}
