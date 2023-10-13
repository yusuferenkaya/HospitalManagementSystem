using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HospitalManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private object userValidator;
        LogManager logManager = new LogManager(new EfLogDal());

        // GET: Login
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)//admin and doctor
        {
            Context c = new Context();

            var adminUserInfo = c.Admins.FirstOrDefault(x => x.adminEmail == p.adminEmail &&
            x.adminPassword == p.adminPassword);

            if(adminUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminUserInfo.adminEmail,false);
                Session["AdminUserName"] = adminUserInfo.adminEmail;


                return RedirectToAction("Index", "AdminDepartment");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult PatientLogin()
        {

            return View();
        }


        [HttpPost]
        public ActionResult PatientLogin(User p)
        {
            Context c = new Context();

            var patientUserInfo = c.Users.FirstOrDefault(x => x.userEmail == p.userEmail &&
            x.userPassword == p.userPassword);

            if (patientUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(patientUserInfo.userEmail, false);
                Session["PatientUserEmail"] = patientUserInfo.userEmail;
                var log = new Log { Email = (string)Session["PatientUserEmail"], Message = patientUserInfo.userPreName + " " + patientUserInfo.userLastName + " SIGNED IN", CreatedDate = DateTime.Now }; logManager.LogAdd(log);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "The login credentials are incorrect. Please try again." });
            }
        }

        [HttpPost]
        public ActionResult PatientSignUp(User user, Patient patient)//patient
        {
            UserValidator userValidator = new UserValidator();
            UserManager userManager = new UserManager(new EfUserDal());
            PatientManager patientManager = new PatientManager(new EfPatientDal());
            ValidationResult results = userValidator.Validate(user);

            if (results.IsValid)
            {
                userManager.UserAdd(user);
                patient.userID = user.userID;
                patient.patientPreName = user.userPreName;
                patient.patientLastName = user.userLastName;
                patient.patientEmail = user.userEmail;
                patientManager.PatientAdd(patient);

                var log = new Log { Email = (string)Session["PatientUserEmail"], Message = user.userPreName + " " + user.userLastName + " SIGNED UP", CreatedDate = DateTime.Now }; logManager.LogAdd(log);

                return RedirectToAction("PatientLogin");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["PatientUserEmail"], Message = user.userPreName + " " + user.userLastName + "FAILED TO SIGN UP", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();



        }


    }
}