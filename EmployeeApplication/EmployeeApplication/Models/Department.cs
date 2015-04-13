using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> EmployeeList { get; set; }
    }
}