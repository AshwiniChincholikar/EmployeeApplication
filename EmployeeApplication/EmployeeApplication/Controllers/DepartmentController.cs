using EmployeeApplication.DAL;
using EmployeeApplication.Models;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System;
using System.Data.Entity;

namespace EmployeeApplication.Controllers
{
    public class DepartmentController : Controller
    {
        private EmployeeContext db = new EmployeeContext();
        // GET: Department
        public ActionResult Index()
        {
            var department = db.Department.ToList();
            return View(department);
        }

        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment(Department deptartment)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(deptartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deptartment);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = db.Department.Find(id);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Department department)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dep = db.Department.Find(id);
            if (TryUpdateModel(dep, "", new string[] { "DepartmentId", "DepartmentName", "EmployeeList" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to Update");
                }
            }
            return View(department);
        }
        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = db.Department.Find(id); 
            if (department == null)
            {
                return HttpNotFound();
            }            
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartment(int id, Department department)
        {
            var dep = db.Department.Find(id);
            if (dep == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);                
            }
            var employeeList = db.Employee.Where(e => e.DepartmentId == department.Id);
            foreach (var item in employeeList)
            {
                db.Employee.Remove(item);
            }
            db.Department.Remove(dep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeparmentEmployeesList(int id)
        {
            var list = db.Employee.Where(e => e.DepartmentId == id).ToList();
            if (list.Count() == 0)
            {
                var link = Url.Action("Index", "Department", "", protocol: Request.Url.Scheme);
                ViewBag.ErrorMessage = "No Employees Associated with this department. ";
                ViewBag.Link = link;
                return View("Error");
            }
            Department department = db.Department.Where(d => d.Id == id).ToList().First();
            department.EmployeeList = list;
            return View(department);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}