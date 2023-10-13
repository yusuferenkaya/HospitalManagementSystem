using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentHourController : Controller
    {

        LogManager logManager = new LogManager(new EfLogDal());
        AppointmentHourManager appointmentHourManager = new AppointmentHourManager(new EfAppointmentHourDal());
        AppointmentHourValidator appointmentHourValidator = new AppointmentHourValidator();
        // GET: AppointmentHour
        public ActionResult Index()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE APPONTMENT HOURS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            var appointmentHours = appointmentHourManager.GetList();
            return View(appointmentHours);
        }

        [Authorize]
        public ActionResult DeleteAppointmentHour(int id)
        {

            var appointmentHourValue = appointmentHourManager.GetByID(id);
            appointmentHourManager.AppointmentHourDelete(appointmentHourValue);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN REMOVED THE APPOINTMENT HOUR : Hour: " + appointmentHourValue.Hour + " id is " + appointmentHourValue.Id, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public ActionResult UpdateAppointmentHour(int id)
        {
            var appointmentHourValue = appointmentHourManager.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING AN APPOINTMENT HOUR PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(appointmentHourValue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateAppointmentHour(AppointmentHour appointmentHour)
        {

            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE APPONTMENT HOUR : Hour: " + appointmentHour.Hour + " id is " + appointmentHour.Id, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            appointmentHourManager.AppointmentHourUpdate(appointmentHour);
            return RedirectToAction("Index");
        }



        public ActionResult AddAppointmentHour()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING AN NEW APPOINTMENT HOUR PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddAppointmentHour(AppointmentHour appointmentHour)
        {
            //doctor.birthDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = appointmentHourValidator.Validate(appointmentHour);

            if (results.IsValid)
            {
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED A APPOINTMENT HOUR : Hour: " + appointmentHour.Hour + " id is " + appointmentHour.Id, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                appointmentHourManager.AppointmentHourAdd(appointmentHour);
                return RedirectToAction("Index");
            }
            else
            {

                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A APPOINTMENT HOUR : Hour: " + appointmentHour.Hour, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }


    }
}