namespace EmployeeApplication.Migrations
{
    using EmployeeApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeApplication.DAL.EmployeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EmployeeApplication.DAL.EmployeeContext";
        }

        protected override void Seed(EmployeeApplication.DAL.EmployeeContext context)
        {
            context.Department.AddOrUpdate(
                p => p.Id,
                new Department { DepartmentName = "Accounts" },
                new Department { DepartmentName = "Sales" },
                new Department { DepartmentName = "Marketing" },
                new Department { DepartmentName = "Human Resource" }
                );

            context.Employee.AddOrUpdate(
                p => p.Id,
                new Employee { EmployeeName = "Rohit", Salary = 200000, DepartmentId = 1 },
                new Employee { EmployeeName = "Rohan", Salary = 250000, DepartmentId = 1 },
                new Employee { EmployeeName = "Sallie", Salary = 220000, DepartmentId = 2 },
                new Employee { EmployeeName = "Bob", Salary = 250000, DepartmentId = 3 },
                new Employee { EmployeeName = "Scott", Salary = 250000, DepartmentId = 4 }
                );
            context.SaveChanges();
        }
    }
}
