using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPIProject.Models;
using WebAPIProject.Utlility;

namespace WebAPIProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Search(string searchCriteria)
        {
            if (searchCriteria == null)
            {
                return Json(new JsonMessage("Failure", "SearchCriteria is null"), JsonRequestBehavior.AllowGet);
            }
            List<Department> departments = db.Departments.Where(d => d.Name.Contains(searchCriteria)).ToList();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }




        public ActionResult List()
        {
            return Json(db.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }
        //should be called with Department/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
           Department department = db.Departments.Find(id);
            if (department == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(department, JsonRequestBehavior.AllowGet);

        } //Employees/Create  [POST]
        public ActionResult Create([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Departments.Add(department);
            try
            {
               db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonMessage("Success", "Employee was created the new id is:"), JsonRequestBehavior.AllowGet); //add employee id return data





        }





        public ActionResult Remove([FromBody] Employee employee)
        {
            Department department2 = db.Departments.Find(employee.Id);
            db.Departments.Remove(department2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was deleted successfully"), JsonRequestBehavior.AllowGet);
        }
        //Employees/Change
        public ActionResult Change([FromBody] Department department)
        {
            if (department == null)
            {
                return Json(new JsonMessage("Failure", "The record has already been deleted,not found"), JsonRequestBehavior.AllowGet);
            }
            Department department2 = db.Departments.Find(department.Id);
            department2.Id = department.Id;
            department2.Name = department.Name;
            department2.EmployeeId = department.EmployeeId;
            department2.Budget = department.Budget;
            try
            {
               // db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was changed"), JsonRequestBehavior.AllowGet);
        }
    }
}