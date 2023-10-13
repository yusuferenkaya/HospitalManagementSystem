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
    public class AdminStaffController : Controller
    {
        // GET: AdminStaff
        LogManager logManager = new LogManager(new EfLogDal());
        StaffManager staffManager = new StaffManager(new EfStaffDal());
        //UserManager userManager = new UserManager(new EfUserDal());
        StaffValidator staffValidator = new StaffValidator();

        [Authorize]
        public ActionResult Index()
        {
            var staffvalues = staffManager.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE STAFF", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(staffvalues);
        }


        [Authorize]
        public ActionResult IndexInTableFormat()
        {
            var staffvalues = staffManager.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE STAFF", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(staffvalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddStaff()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING A NEW STAFF PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddStaff(Staff staff)
        {
            //staff.birthDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = staffValidator.Validate(staff);

            if (results.IsValid)
            {
                staffManager.StaffAdd(staff);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED A NEW STAFF : Staff Name: " + staff.staffPreName + " " + staff.staffLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A NEW STAFF : Staff Name: " + staff.staffPreName + " " + staff.staffLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult DeleteStaff(int id)
        {
            var staffvalue = staffManager.GetByID(id);
            staffManager.StaffDelete(staffvalue);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN REMOVED THE STAFF : Staff Name: " + staffvalue.staffPreName + " " + staffvalue.staffLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateStaff(int id)
        {
            var staffvalue = staffManager.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING A STAFF PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(staffvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateStaff(Staff staff)
        {
            ValidationResult results = staffValidator.Validate(staff);

            if (results.IsValid)
            {
                staffManager.StaffUpdate(staff);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE STAFF : Staff Name: " + staff.staffPreName + " " + staff.staffLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO UPDATE THE STAFF : Staff Name: " + staff.staffPreName + " " + staff.staffLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }
    }
}