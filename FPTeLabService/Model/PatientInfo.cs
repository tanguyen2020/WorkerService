using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabService
{
    public class PatientInfo
    {
        public int? PatientID { get; set; }
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public int? YOB { get; set; }
        public string Sex { get; set; }
        public string Address2 { get; set; }
        public string Address { get; set; }
        public string HospitalCode { get; set; }
        public int? PatientID_His { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
