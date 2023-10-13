using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AdminPrescriptionController : Controller
    {
        // GET: AdminPrescription
        LogManager logManager = new LogManager(new EfLogDal());
        PrescriptionManager prescriptionManager = new PrescriptionManager(new EfPrescriptionDal());
        DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
        PrescriptionValidator prescriptionValidator = new PrescriptionValidator();
        AppointmentManager appointmentManager = new AppointmentManager(new EfAppointmentDal());
        [Authorize]
        public ActionResult Index()
        {
            var prescriptionvalues = prescriptionManager.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE PRESCRIPTIONS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(prescriptionvalues);
        }

        [Authorize]
        public ActionResult DoctorDisplayPrescription()
        {
            // AppointmentManager appointmentManager = new AppointmentManager(new EfAppointmentDal());
            DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
            int doctorIDCurrent = doctorManager.GetByEmail((string)Session["AdminUserName"]).doctorID;
            // List<Appointment> appointmentListForCurrentDoctor = appointmentManager.GetListByID(doctorIDCurrent);

            using (Context context = new Context())
            {
                string sql = "SELECT medicineName,prescriptionID, duration, useIntr, patientPreName, patientLastName FROM doctor_prescriptions WHERE doctorID = @doctorID";
                SqlParameter parameter = new SqlParameter("@doctorID", doctorIDCurrent);
                var result = context.Database.SqlQuery<Deneme>(sql, parameter).ToList();
                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR DISPLAYED THE PRESCRIPTIONS : Doctor Name: " + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return View(result);
            }
        }



        [Authorize]
        public ActionResult PatientDisplayPrescription()
        {
            PatientManager patientManager = new PatientManager(new EfPatientDal());
            Patient patientCurrent = patientManager.GetByEmail((string)Session["PatientUserEmail"]);
            int patientIDCurrent = patientCurrent.patientID;



            using (Context context = new Context())
            {
                var result = (from p in context.Prescriptions
                              join a in context.Appointments on p.appointmentID equals a.appointmentID
                              join h in context.Patients on a.patientID equals h.patientID
                              where h.patientID == patientIDCurrent
                              select new Deneme
                              {
                                  medicineName = p.medicineName,
                                  prescriptionID = p.prescriptionID,
                                  duration = p.duration,
                                  useIntr = p.useIntr,
                                  patientPreName = h.patientPreName,
                                  patientLastName = h.patientLastName
                              }).ToList();
                var log = new Log { Email = (string)Session["PatientUserEmail"], Message = "PATIENT DISPLAYED HIS/HER PRESCRIPTIONS : Patient Name: " + patientCurrent.patientPreName + " " +patientCurrent.patientLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return View(result);

            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPrescription()
        {
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            List<SelectListItem> valueappointment = (from x in appointmentManager.GetList()
                                                     where x.doctorID.Equals(currentDoctor.doctorID)
                                                     select new SelectListItem
                                                    {
                                                        Text = x.Patient.patientPreName + " " + x.Patient.patientLastName,
                                                        Value = x.appointmentID.ToString()
                                                    }).ToList();
            ViewBag.appointmentViewBag = valueappointment;
            if (User.IsInRole("1"))
            {
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADD NEW PRESCRIPTION PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            }

            if (User.IsInRole("2"))
            {
                currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR ENTERED THE ADD NEW PRESCRIPTION PAGE : Doctor Name: " + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            }
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddPrescription(Prescription prescription)
        {
            //doctor.birthDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = prescriptionValidator.Validate(prescription);
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            if (!ModelState.IsValid)
            {
                List<SelectListItem> valueappointment = (from x in appointmentManager.GetList()
                                                         where x.doctorID.Equals(currentDoctor.doctorID)
                                                         select new SelectListItem
                                                         {
                                                             Text = x.Patient.patientPreName,
                                                             Value = x.appointmentID.ToString()
                                                         }).ToList();
                ViewBag.appointmentViewBag = valueappointment;

            }
            if (results.IsValid)
            {
                //logger.Info("Admin add new doctor: " + doctor.doctorPreName + " " + doctor.doctorLastName);
                prescriptionManager.PrescriptionAdd(prescription);
                if (User.IsInRole("1"))
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED NEW PRESCRIPTION : "
                        , CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    return RedirectToAction("Index");

                }

                if (User.IsInRole("2"))
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR ADDED NEW PRESCRIPTION: Doctor Name: " +
                        currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    return RedirectToAction("DoctorDisplayPrescription");
                }
                
            }
            else
            {

                foreach (var item in results.Errors)
                {
                    if (User.IsInRole("1"))
                    {
                        var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD NEW PRESCRIPTION", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        return RedirectToAction("Index");
                    }

                    if (User.IsInRole("2"))
                    {
                        var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR FAILED TO ADD NEW PRESCRIPTION: Doctor Name: " + 
                            currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        return RedirectToAction("DoctorDisplayPrescription");
                    }
                }
            }

            return View();
        }


        [Authorize]
        public ActionResult DeletePrescription(int id)
        {
            
            var prescriptionvalue = prescriptionManager.GetByID(id);

            if (User.IsInRole("1"))
            {

                prescriptionManager.PrescriptionDelete(prescriptionvalue);
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "ADMUN DELETED THE PRESCRIPTION",
                    CreatedDate = DateTime.Now
                }; logManager.LogAdd(log);
                return RedirectToAction("Index");

            }

            if (User.IsInRole("2"))
            {
                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                prescriptionManager.PrescriptionDelete(prescriptionvalue);
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "DOCTOR DELETED THE PRESCRIPTION : Doctor Name: " + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                    CreatedDate = DateTime.Now
                }; logManager.LogAdd(log);
                return RedirectToAction("DoctorDisplayPrescription");
            }

            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult UpdatePrescription(int id)
        {
            var prescriptionvalue = prescriptionManager.GetByID(id);
            if (User.IsInRole("1"))
            {
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED UPDATING PRESCRIPTION PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            }

            if (User.IsInRole("2"))
            {
                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR ENTERED UPDATING PRESCRIPTION PAGE : Doctor Name: " + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            }

            return View(prescriptionvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdatePrescription(Prescription prescription)
        {

            prescriptionManager.PrescriptionUpdate(prescription);
            if (User.IsInRole("1"))
            {
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED UPDATING PRESCRIPTION PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }

            if (User.IsInRole("2"))
            {
                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "DOCTOR UPDATED THE PRESCRIPTION : Doctor Name: " + currentDoctor.doctorPreName + " " + 
                    currentDoctor.doctorLastName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("DoctorDisplayPrescription");
            }

            return View();

        }

    }
}