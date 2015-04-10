using EmployeeApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeApplication.Models;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private EmployeeContext db = new EmployeeContext();
        public ActionResult GetEmployees()
        {
            var employeeList = db.Employee.ToList();
            var department = db.Department.ToList();
            ViewBag.department = department;
            return View("Index", employeeList);
        }

        public ActionResult CreateEmployee()
        {
            List<Department> dd = db.Department.ToList();
            Employee e = new Employee();
            e.Department = dd;
            ViewBag.Dep = e;
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("GetEmployees");
            }

            return View(employee);
        }

        public ActionResult EmployeeDetails(int id)
        {
            var employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var department = db.Department.ToList();
            employee.Department = department;
            ViewBag.department = department;
            return View("EmployeeDetails", employee);
        }
        public ActionResult Edit(int id)
        {
            List<Department> department = db.Department.ToList();
            Employee employee = db.Employee.Find(id);
            employee.Department = department;
            ViewBag.Dep = employee;
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            var editEmployee = db.Employee.Find(id);
            if (TryUpdateModel(editEmployee))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("GetEmployees");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to Update");
                }
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult Delete(int id)
        {
            var employee = db.Employee.Find(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            var employeeToDelete = db.Employee.Find(id);
            db.Employee.Remove(employeeToDelete);
            db.SaveChanges();
            return RedirectToAction("GetEmployees");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}