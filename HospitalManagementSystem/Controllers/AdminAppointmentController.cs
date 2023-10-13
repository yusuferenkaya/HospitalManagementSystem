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
    public class AdminAppointmentController : Controller
    {
        // GET: AdminAppointment
        LogManager logManager = new LogManager(new EfLogDal());
        AppointmentManager appointmentManager = new AppointmentManager(new EfAppointmentDal());
        DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
        AppointmentValidator appointmentValidator = new AppointmentValidator();
        DepartmentManager departmentManager = new DepartmentManager(new EfDepartmentDal());
        [Authorize]
        public ActionResult Index()
        {
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            var log = new Log{Email = (string)Session["AdminUserName"],Message = "ADMIN DISPLAYED THE APPOINTMENTS", CreatedDate = DateTime.Now};logManager.LogAdd(log);
            var appointmentvalues = appointmentManager.GetList();
            return View(appointmentvalues);
        }

        [HttpGet]
        public ActionResult MakeAppointment()
        {

            List<SelectListItem> valuedepartment = (from x in departmentManager.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.departmentName,
                                                        Value = x.departmentID.ToString()
                                                    }).ToList();
            ViewBag.departmentViewBag = valuedepartment;

            List<SelectListItem> valuedoctor= (from x in doctorManager.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.doctorPreName + " " + x.doctorLastName,
                                                        Value = x.doctorID.ToString()
                                                    }).ToList();
            ViewBag.doctorViewBag = valuedoctor;
            return View();
        }


        public JsonResult GetDoctors(int p)
        {
            Context context = new Context();
            var doctors = (from x in context.Doctors
                           join y in context.Departments on x.departmentID equals y.departmentID
                           where x.Department.departmentID == p
                           select new
                           {
                               Text = x.doctorPreName+ " " + x.doctorLastName,
                               Value = x.doctorID.ToString()
                           }).ToList();

            return Json(doctors, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetAppointmentHours(int doctorId, DateTime date)
        {
            // Retrieve the available appointment hours for the selected doctor from the database
            var appointmentHours = GetAppointmentHoursFromDatabase(doctorId,date);

            // Return the available appointment hours as a JSON object
            return Json(appointmentHours);
        }

        private List<AppointmentHour> GetAppointmentHoursFromDatabase(int doctorId, DateTime date)
        {
            // Establish a connection to the database
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Context"].ConnectionString))
            {
                connection.Open();

                // Execute the stored procedure to retrieve the available appointment hours for the selected doctor
                using (var command = new SqlCommand("GetAvailableAppointmentHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@doctorID", doctorId);
                    command.Parameters.AddWithValue("@appDate", date);

                    // Execute the command and retrieve the data
                    var dataReader = command.ExecuteReader();

                    // Create a list to store the available appointment hours
                    var appointmentHours = new List<AppointmentHour>();

                    // Read the data and add the available appointment hours to the list
                    while (dataReader.Read())
                    {
                        appointmentHours.Add(new AppointmentHour
                        {
                            Id = (int)dataReader["Id"],
                            Hour = (string)dataReader["Hour"]
                        });
                    }

                    // Return the available appointment hours
                    return appointmentHours;
                }
            }
        }

















        [HttpPost]
        public ActionResult MakeAppointment(int departmentId, int doctorId, DateTime date, int appointmentHourId)
        {
            PatientManager patientManager = new PatientManager(new EfPatientDal());
            DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
            DepartmentManager departmentManager = new DepartmentManager(new EfDepartmentDal());
            AppointmentHourManager appointmentHourManager = new AppointmentHourManager(new EfAppointmentHourDal());

            // Create a new Appointment object using the chosen values
            var appointment = new Appointment
            {
                patientID = patientManager.GetByEmail((string)Session["PatientUserEmail"]).patientID,
                doctorID = doctorId,
                appDate = date,
                AppointmentHourID = appointmentHourId

            };

            

            // Validate the Appointment object
            ValidationResult results = appointmentValidator.Validate(appointment);

            if (results.IsValid)
            {
                // Add the Appointment object to the Appointments table
                appointmentManager.AppointmentAdd(appointment);

                // Log the operation
                var log = new Log
                {
                    Email = (string)Session["PatientUserEmail"],
                    Message = "USER MADE AN APPOINTMENT :" + patientManager.GetByEmail((string)Session["PatientUserEmail"]).patientPreName +" "+ patientManager.GetByEmail((string)Session["PatientUserEmail"]).patientLastName,
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);

                int departmentIDS = doctorManager.GetByID(appointment.doctorID).departmentID;
                String hourS = appointmentHourManager.GetByID(appointmentHourId).Hour;
                String appDateS = appointment.appDate.Day +"/" +appointment.appDate.Month + "/" + appointment.appDate.Year + " Hour: " + hourS;
                String doctorNameS = doctorManager.GetByID(appointment.doctorID).doctorPreName + " " + doctorManager.GetByID(appointment.doctorID).doctorLastName;
                String departmentNameS = departmentManager.GetByID(departmentIDS).departmentName;
                return Json(new { success = true, appointmentDate = appDateS, department = departmentNameS, doctorName = doctorNameS});
            }
            else
            {
                // Log the failure
                var log = new Log
                {
                    Email = (string)Session["PatientUserEmail"],
                    Message = "USER FAILED TO MAKE AN APPOINMENT :" + patientManager.GetByEmail((string)Session["PatientUserEmail"]).patientPreName +" "+ patientManager.GetByEmail((string)Session["PatientUserEmail"]).patientLastName,
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);

                // Add the validation errors to the ModelState
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

                // Return the View with the validation errors
                return View();
            }
        }


        [Authorize]
        public ActionResult DoctorDisplayAppointment()
        {
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR DISPLAYED THE APPOINTMENTS : " + currentDoctor.doctorPreName + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            List<EntityLayer.Concrete.Appointment> listAppointment = appointmentManager.GetListByID(doctorManager.GetByEmail((string)Session["AdminUserName"]).doctorID);
            return View(listAppointment);
        }

        [Authorize]
        public ActionResult PatientDisplayAppointment()
        {
            PatientManager patientManager = new PatientManager(new EfPatientDal());
            Patient currentPatient = patientManager.GetByEmail((string)Session["PatientUserEmail"]);
            var log = new Log { Email = (string)Session["PatientUserEmail"], Message = "PATIENT DISPLAYED HIS/HER APPOINTMENTS : " + currentPatient.patientPreName + currentPatient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            List<EntityLayer.Concrete.Appointment> listAppointment = appointmentManager.GetListByIDForPatient(currentPatient.patientID);
            return View(listAppointment);
        }




        [Authorize]
        public ActionResult DeleteAppointment(int id)
        {
            var appointmentvalue = appointmentManager.GetByID(id);
            Doctor currentDoctor = doctorManager.GetByID(appointmentvalue.doctorID);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DELETE THE APPOINTMENT : Doctor Name:" + currentDoctor.doctorPreName + currentDoctor.doctorLastName + "Patient Name: "+ appointmentvalue.Patient.patientPreName + " " + appointmentvalue.Patient.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            appointmentManager.AppointmentDelete(appointmentvalue);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateAppointment(int id)
        {
            var appointmentvalue = appointmentManager.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING APPOINMENT PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(appointmentvalue);
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpdateAppointment(Appointment appointment)
        {
            ValidationResult results = appointmentValidator.Validate(appointment);

            if (results.IsValid)
            {
                appointmentManager.AppointmentUpdate(appointment);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE APPOINTMENT : Appointment ID:" + appointment.appointmentID, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO UPDATE THE APPOINTMENT : Appointment ID:" + appointment.appointmentID, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();
        }
    }

}