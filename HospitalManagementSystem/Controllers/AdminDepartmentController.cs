using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AdminDepartmentController : Controller
    {
        // GET: AdminDepartment

        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        LogManager lm = new LogManager(new EfLogDal());
        DepartmentValidator departmentValidator = new DepartmentValidator();

        [Authorize]
        public ActionResult Index()
        {
            var log = new Log
            {
                Email = (string)Session["AdminUserName"],
                Message = "ADMIN DISPLAYED THE DEPARTMENTS",
                CreatedDate = DateTime.Now
            };
            lm.LogAdd(log);

            var departmentvalues = dm.GetList();
            return View(departmentvalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddDepartment(Department department)
        {
            ValidationResult results = departmentValidator.Validate(department);

            if (results.IsValid)
            {
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "ADMIN ADDED A NEW DEPARTMENT :" + department.departmentName,
                    CreatedDate = DateTime.Now
                };
                lm.LogAdd(log);
                dm.DepartmentAdd(department);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log
                    {
                        Email = (string)Session["AdminUserName"],
                        Message = "ADMIN FAILED TO ADD A NEW DEPARTMENT :" + department.departmentName,
                        CreatedDate = DateTime.Now
                    };
                    lm.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult DeleteDepartment(int id)
        {
            var departmentvalue = dm.GetByID(id);
            dm.DepartmentDelete(departmentvalue);
            var log = new Log
            {
                Email = (string)Session["AdminUserName"],
                Message = "ADMIN REMOVED THE DEPARTMENT : Department Name: " + departmentvalue.departmentName,
                CreatedDate = DateTime.Now
            };
            lm.LogAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateDepartment(int id)
        {
            var departmentvalue = dm.GetByID(id);
            return View(departmentvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateDepartment(Department department)
        {
            ValidationResult results = departmentValidator.Validate(department);

            if (results.IsValid)
            {
                dm.DepartmentUpdate(department);
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "ADMIN UPDATED THE DEPARTMENT : Department Name: " + department.departmentName,
                    CreatedDate = DateTime.Now
                };
                lm.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log
                    {
                        Email = (string)Session["AdminUserName"],
                        Message = "ADMIN FAILED TO UPDATE THE DEPARTMENT : Department Name: " + department.departmentName,
                        CreatedDate = DateTime.Now
                    };
                    lm.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }
    }
}