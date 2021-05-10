using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class PatientTypeInfo
    {
        public int PatientType_Id { get; set; }
        public string PatientTypeCode { get; set; }
        public string PatientTypeName { get; set; }
        public string PatientTypeGroup { get; set; }
        public bool Enabled { get; set; }
    }
}
