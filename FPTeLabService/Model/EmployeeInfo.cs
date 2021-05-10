using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Dapper;

namespace FPTeLabService
{
    public class EmployeeInfo
    {
        public int? Employee_ID { get; set; }
        public int? DepartmentID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Sex { get; set; }
        public DateTime? DOB { get; set; }
        public bool Enabled { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
    }
}
