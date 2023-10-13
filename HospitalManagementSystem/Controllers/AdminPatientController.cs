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
    public class AdminPatientController : Controller
    {
        // GET: AdminPatient

        LogManager logManager = new LogManager(new EfLogDal());
        PatientManager patientManager = new PatientManager(new EfPatientDal());
        UserManager userManager = new UserManager(new EfUserDal());
        DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
        AppointmentManager appointmentManager = new AppointmentManager(new EfAppointmentDal());
        PatientValidator patientValidator = new PatientValidator();
        [Authorize]
        public ActionResult Index()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE PATIENTS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            var patientvalues = patientManager.GetList();
            return View(patientvalues);
        }

        [Authorize]
        public ActionResult IndexInTableFormat()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE PATIENTS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            var patientvalues = patientManager.GetList();
            return View(patientvalues);
        }
        [Authorize]
        public ActionResult DoctorDisplayPatient()
        {
            int doctorIDCurrent = doctorManager.GetByEmail((string)Session["AdminUserName"]).doctorID;
            List<Appointment> appointmentListForCurrentDoctor = appointmentManager.GetListByID(doctorIDCurrent);
            List<Patient> listPatients = new List<Patient>();
            for (int i = 0; i < appointmentListForCurrentDoctor.Count; i++)
            {
                listPatients.Add(appointmentListForCurrentDoctor[i].Patient);
            }
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR DISPLAYED THE PATIENTS : " + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(listPatients);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddPatient()
        {
            List<SelectListItem> valueuser = (from x in userManager.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.userPreName +" " +x.userLastName,
                                                  Value = x.userID.ToString()
                                              }).ToList();
            ViewBag.userViewBag = valueuser;
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING A NEW PATIENT PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddPatient(Patient patient)
        {
            //patient.birthDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = patientValidator.Validate(patient);

            if (results.IsValid)
            {
                patientManager.PatientAdd(patient);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADD A NEW PATIENT : " + patient.patientPreName + " " + patient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A NEW PATIENT : " + patient.patientPreName + " " + patient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult DeletePatient(int id)
        {
            var patientvalue = patientManager.GetByID(id);
            UserManager userManager = new UserManager(new EfUserDal());
            userManager.UserDelete(userManager.GetByID(patientvalue.userID));
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DELETED  THE PATIENT : " + "Patient Name: " + patientvalue.patientPreName + " " + patientvalue.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdatePatient(int id)
        {
            List<SelectListItem> valueuser = (from x in userManager.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.userPreName + " " + x.userLastName,
                                                  Value = x.userID.ToString()
                                              }).ToList();
            ViewBag.userViewBag = valueuser;
            var patientvalue = patientManager.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING A DOCTOR PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(patientvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdatePatient(Patient patient)
        {
            ValidationResult results = patientValidator.Validate(patient);

            if (results.IsValid)
            {
                patientManager.PatientUpdate(patient);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE PATIENT : " + patient.patientPreName + " " + patient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO UPDATE THE PATIENT : " + patient.patientPreName + " " + patient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }
    }
}