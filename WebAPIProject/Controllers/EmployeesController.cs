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
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult List()
        {
            return Json(db.Employees.ToList(), JsonRequestBehavior.AllowGet);
        }
        //should be called with Employees/Get/5
        public ActionResult Get(int? id)
        {
            if(id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Employee employee = db.Employees.Find(id);
            if(employee == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
       
        } //Employees/Create  [POST]
        public ActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();
            }catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonMessage("Success", "Employee was created the new id is:"), JsonRequestBehavior.AllowGet); //add employee id return data


           


            }





        public ActionResult Remove([FromBody] Employee employee)
        {
            Employee employee2 = db.Employees.Find(employee.Id);
            db.Employees.Remove(employee2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was deleted successfully"), JsonRequestBehavior.AllowGet);
        }
            //Employees/Change
            public ActionResult Change([FromBody] Employee employee)
            {
                Employee employee2 = db.Employees.Find(employee.Id);
                employee2.Name = employee.Name;
                employee2.Salary = employee.Salary;
                employee2.Active = employee.Active;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonMessage("Success", "Employee was changed"), JsonRequestBehavior.AllowGet);
            }
        }
}