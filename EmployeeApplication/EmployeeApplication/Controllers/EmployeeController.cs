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
            ViewBag.Dep = department;
            return View("Index", employeeList);
        }

        public ActionResult CreateEmployee()
        {
            IEnumerable<SelectListItem> departments = db.Department
                                                  .Select(d => new SelectListItem
                                                     {
                                                         Value = d.Id.ToString(),
                                                         Text = d.DepartmentName
                                                     });
            ViewBag.Department = departments;
            return View();
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
            IEnumerable<SelectListItem> departments = db.Department
                                                 .Select(d => new SelectListItem
                                                 {
                                                     Value = d.Id.ToString(),
                                                     Text = d.DepartmentName
                                                 });

            ViewBag.Department = departments;
            return View("EmployeeDetails", employee);
        }
        public ActionResult Edit(int id)
        {
            Employee employee = db.Employee.Find(id);
            IEnumerable<SelectListItem> departments = db.Department
                                                 .Select(d => new SelectListItem
                                                 {
                                                     Value = d.Id.ToString(),
                                                     Text = d.DepartmentName,
                                                     // Selected = true
                                                 });

            ViewBag.Department = departments;
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
            if (employee != null)
            {
                return View(employee);
            }
            else
            {
                return HttpNotFound();
            }
           
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            var employeeToDelete = db.Employee.Find(id);
            if (employeeToDelete != null)
            {
                db.Employee.Remove(employeeToDelete);
                db.SaveChanges();
                return RedirectToAction("GetEmployees");
            }
            else
            {
                return HttpNotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}