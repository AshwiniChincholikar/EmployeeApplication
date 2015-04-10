using System.Collections.Generic;

namespace EmployeeApplication.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> EmployeeList { get; set; }
    }
}