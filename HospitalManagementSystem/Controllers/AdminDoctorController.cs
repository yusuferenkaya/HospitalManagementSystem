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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;

namespace HospitalManagementSystem.Controllers
{
    public class AdminDoctorController : Controller
    {
        // GET: AdminDoctor
        LogManager logManager = new LogManager(new EfLogDal());
        DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
        DepartmentManager departmentManager = new DepartmentManager(new EfDepartmentDal());
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        //UserManager userManager = new UserManager(new EfUserDal());
        DoctorValidator doctorValidator = new DoctorValidator();
        [Authorize]
        public ActionResult Index()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE DOCTORS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            var doctorvalues = doctorManager.GetList();
            return View(doctorvalues);
        }
        [Authorize]
        public ActionResult IndexInTableFormat()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE DOCTORS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            var doctorvalues = doctorManager.GetList();
            return View(doctorvalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddDoctor()
        {
            List<SelectListItem> valuedepartment = (from x in departmentManager.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.departmentName,
                                                        Value = x.departmentID.ToString()
                                                    }).ToList();
            ViewBag.departmentViewBag = valuedepartment;

            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING A NEW DOCTOR PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddDoctor(Doctor doctor)
        {
            //doctor.birthDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = doctorValidator.Validate(doctor);

            if (results.IsValid)
            {
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED A NEW DOCTOR : Doctor Name: " + doctor.doctorPreName + " " + doctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                doctorManager.DoctorAdd(doctor);
                Admin admin = new Admin();
                admin.adminEmail = doctor.doctorEmail;
                admin.userRole = "2";
                string passwordPart = ConvertTurkishCharacters(doctor.doctorLastName);
                admin.adminPassword = passwordPart+ doctor.socSecNo;
                adminManager.AdminAdd(admin);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A NEW DOCTOR : " + doctor.doctorPreName + " " + doctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            List<SelectListItem> valuedepartment = (from x in departmentManager.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.departmentName,
                                                        Value = x.departmentID.ToString()
                                                    }).ToList();
            ViewBag.departmentViewBag = valuedepartment;
            return View();
        }

        [Authorize]
        public ActionResult DeleteDoctor(int id)
        {

            var doctorvalue = doctorManager.GetByID(id);
            doctorManager.DoctorDelete(doctorvalue);
            adminManager.AdminDelete(adminManager.GetByEmail(doctorvalue.doctorEmail));
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN REMOVED THE DOCTOR : Doctor Name: " + doctorvalue.doctorPreName + " " + doctorvalue.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateDoctor(int id)
        {
            List<SelectListItem> valuedepartment = (from x in departmentManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.departmentName,
                                                       Value = x.departmentID.ToString()
                                                   }).ToList();
            ViewBag.departmentViewBag = valuedepartment;
            var doctorvalue = doctorManager.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING A DOCTOR PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(doctorvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateDoctor(Doctor doctor)
        {

            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE DOCTOR : " + doctor.doctorPreName + " " + doctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            doctorManager.DoctorUpdate(doctor);
            //logger.Info("Admin deleted the doctor: " + doctor.doctorPreName + " " + doctor.doctorLastName);
            return RedirectToAction("Index");
        }

        public int CalculateAge(DateTime birthDate)
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Calculate the difference between the current date and the birth date in terms of years
            int age = currentDate.Year - birthDate.Year;

            // If the current date is before the birth date's month and day, subtract one from the age
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }

        public static string ConvertTurkishCharacters(string s)
        {
            string result = "";
            foreach (char c in s)
            {
                if (c == 'Ğ')
                {
                    result += 'G';
                }
                else if (c == 'Ö')
                {
                    result += 'O';
                }
                else if (c == 'Ü')
                {
                    result += 'U';
                }
                else if (c == 'Ş')
                {
                    result += 'S';
                }
                else if (c == 'Ç')
                {
                    result += 'C';
                }
                else if (c == 'İ')
                {
                    result += 'I';
                }
                else if (c == 'ı')
                {
                    result += 'i';
                }
                else if (c == 'ğ')
                {
                    result += 'g';
                }
                else if (c == 'ö')
                {
                    result += 'o';
                }
                else if (c == 'ü')
                {
                    result += 'u';
                }
                else if (c == 'ş')
                {
                    result += 's';
                }
                else if (c == 'ç')
                {
                    result += 'c';
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }





    }
}