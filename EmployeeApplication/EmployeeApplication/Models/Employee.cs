using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeApplication.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}