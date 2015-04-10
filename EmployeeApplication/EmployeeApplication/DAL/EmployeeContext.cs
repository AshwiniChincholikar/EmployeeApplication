using EmployeeApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EmployeeApplication.DAL
{
    public class EmployeeContext : DbContext
    {
        [Key]
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }       
      
    }
}