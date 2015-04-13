using EmployeeApplication.Models;
using System.Data.Entity;

namespace EmployeeApplication.DAL
{
    public class EmployeeContext : DbContext
    {        
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }       
      
    }
}