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
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        LogManager logManager = new LogManager(new EfLogDal());
        UserManager um = new UserManager(new EfUserDal());
        UserValidator userValidator = new UserValidator();
        [Authorize]
        public ActionResult Index()
        {
            var uservalues = um.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE USERS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(uservalues);
        }

        [Authorize]
        public ActionResult IndexInTableFormat()
        {
            var uservalues = um.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE USERS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(uservalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddUser()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING A NEW USER PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            ValidationResult results = userValidator.Validate(user);

            if (results.IsValid)
            {
                um.UserAdd(user);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED A NEW USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A NEW USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult DeleteUser(int id)
        {
            var uservalue = um.GetByID(id);
            um.UserDelete(uservalue);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN REMOVED THE USER : User Email: " + uservalue.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            var uservalue = um.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING A USER PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(uservalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            PatientManager patientManager = new PatientManager(new EfPatientDal());
            ValidationResult results = userValidator.Validate(user);

            if (results.IsValid)
            {
                patientManager.GetByUserID(user.userID).patientEmail = user.userEmail;
                patientManager.GetByUserID(user.userID).patientPreName = user.userPreName;
                patientManager.GetByUserID(user.userID).patientLastName = user.userLastName;
                um.UserUpdate(user);
                patientManager.PatientUpdate(patientManager.GetByUserID(user.userID));
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index"); 
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO UPDATE THE USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }





        [Authorize]
        [HttpGet]
        public ActionResult UpdateUserByPatient()
        {
            
            User user = um.GetByEmail((string)Session["PatientUserEmail"]);
            var log = new Log { Email = (string)Session["PatientUserEmail"], Message = "USER ENTERED THE EDITING INFORMATIONS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(user);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateUserByPatient(User user)
        {
            ValidationResult results = userValidator.Validate(user);
            PatientManager patientManager = new PatientManager(new EfPatientDal());





            if (results.IsValid)
            {

                Patient currentPatient = patientManager.GetByEmail((string)Session["PatientUserEmail"]);
                um.UserUpdate(user);
                currentPatient.patientEmail = user.userEmail;
                patientManager.PatientUpdate(currentPatient);
                var log = new Log { Email = (string)Session["PatientUserEmail"], Message = "ADMIN UPDATED THE USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("PatientLogin","Login");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["PatientUserEmail"], Message = "ADMIN FAILED TO UPDATE THE USER : User Email: " + user.userEmail, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }

    }
}