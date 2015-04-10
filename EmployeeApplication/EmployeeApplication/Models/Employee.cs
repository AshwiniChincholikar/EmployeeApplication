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
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
        public virtual ICollection<Department> Department { get; set; }


    }
}