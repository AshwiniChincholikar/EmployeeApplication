using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> EmployeeList { get; set; }
    }
}