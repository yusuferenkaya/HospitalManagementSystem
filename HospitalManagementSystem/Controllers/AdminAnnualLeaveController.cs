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
    public class AdminAnnualLeaveController : Controller
    {
        // GET: AdminAnnualLeave
        LogManager logManager = new LogManager(new EfLogDal());
        AnnualLeaveManager alm = new AnnualLeaveManager(new EfAnnualLeaveDal());
        DoctorManager doctorManager = new DoctorManager(new EfDoctorDal());
        AnnualLeaveValidator annualLeaveValidator = new AnnualLeaveValidator();
        [Authorize]
        public ActionResult Index()
        {
            var log = new Log
            {
                Email = (string)Session["AdminUserName"],
                Message = "ADMIN DISPLAYED THE ANNUAL LEAVES",
                CreatedDate = DateTime.Now
            };
            logManager.LogAdd(log);
            var annualLeavevalues = alm.GetList();
            return View(annualLeavevalues);
        }
        [Authorize]
        public ActionResult DoctorDisplayAnnualLeave()
        {

            List<EntityLayer.Concrete.AnnualLeave> listAnnualLeave = alm.GetListByID(doctorManager.GetByEmail((string)Session["AdminUserName"]).doctorID);
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            var log = new Log
            {
                Email = (string)Session["AdminUserName"],
                Message = "DOCTOR DISPLAYED THE ANNUAL LEAVES :" + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                CreatedDate = DateTime.Now
            };
            logManager.LogAdd(log);
            return View(listAnnualLeave);
        }
        [Authorize]
        [HttpGet]
        public ActionResult DoctorAnnualLeave()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult DoctorAnnualLeave(AnnualLeave annualLeave)
        {
            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
            annualLeave.doctorID = doctorManager.GetByEmail((string)Session["AdminUserName"]).doctorID;
            ValidationResult results = annualLeaveValidator.Validate(annualLeave);

            if (results.IsValid)
            {
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "DOCTOR ADDED NEW ANNUAL LEAVE :" + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);
                alm.AnnualLeaveAdd(annualLeave);
                return RedirectToAction("DoctorDisplayAnnualLeave");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log
                    {
                        Email = (string)Session["AdminUserName"],
                        Message = "DOCTOR FAILED TO ADD NEW ANNUAL LEAVE :" + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                        CreatedDate = DateTime.Now
                    };
                    logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        public ActionResult DeleteAnnualLeave(int id)
        {
            var annualLeavevalue = alm.GetByID(id);
            alm.AnnualLeaveDelete(annualLeavevalue);

            if (User.IsInRole("2"))
            {

                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);

                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "DOCTOR DELETED THE ANNUAL LEAVE :" + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);
                return RedirectToAction("DoctorDisplayAnnualLeave");
            }

            if (User.IsInRole("1"))
            {
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "ADMIN DELETED AN ANNUAL LEAVE",
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);
                return RedirectToAction("Index");
            }

            return View();

        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateAnnualLeave(int id)
        {


            var annualLeavevalue = alm.GetByID(id);

            if (User.IsInRole("2"))
            {
                Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "DOCTOR ENTERED ANNUAL LEAVE UPDATE PAGE :" + currentDoctor.doctorPreName + " " + currentDoctor.doctorLastName,
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);
            }

            if (User.IsInRole("1"))
            {
                var log = new Log
                {
                    Email = (string)Session["AdminUserName"],
                    Message = "ADMIN ENTERED ANNUAL LEAVE UPDATE PAGE",
                    CreatedDate = DateTime.Now
                };
                logManager.LogAdd(log);
            }

            return View(annualLeavevalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateAnnualLeave(AnnualLeave annualLeave)
        {
            ValidationResult results = annualLeaveValidator.Validate(annualLeave);

            if (results.IsValid)
            {
                alm.AnnualLeaveUpdate(annualLeave);
                if (User.IsInRole("2"))
                {
                    Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                    var log = new Log
                    {

                        Email = (string)Session["AdminUserName"],
                        Message = "DOCTOR UPDATED ANNUAL LEAVE : Doctor Name: " + doctorManager.GetByID(annualLeave.doctorID).doctorPreName + " " + doctorManager.GetByID(annualLeave.doctorID).doctorLastName,
                        CreatedDate = DateTime.Now
                    };
                    logManager.LogAdd(log);
                    return RedirectToAction("DoctorDisplayAnnualLeave");
                }
                if (User.IsInRole("1"))
                {
                    var log = new Log
                    {
                        Email = (string)Session["AdminUserName"],
                        Message = "ADMIN UPDATED ANNUAL LEAVE : Doctor Name: " + doctorManager.GetByID(annualLeave.doctorID).doctorPreName + " " + doctorManager.GetByID(annualLeave.doctorID).doctorLastName,
                        CreatedDate = DateTime.Now
                    };
                    logManager.LogAdd(log);
                    return RedirectToAction("Index");
                }
                
            }
            else
            {

                    if (User.IsInRole("2"))
                    {
                        foreach (var item in results.Errors)
                        {
                            Doctor currentDoctor = doctorManager.GetByEmail((string)Session["AdminUserName"]);
                            var log = new Log
                            {

                                Email = (string)Session["AdminUserName"],
                                Message = "DOCTOR FAILED TO UPDATE ANNUAL LEAVE : Doctor Name: " + doctorManager.GetByID(annualLeave.doctorID).doctorPreName + " " + doctorManager.GetByID(annualLeave.doctorID).doctorLastName,
                                CreatedDate = DateTime.Now
                            };
                            logManager.LogAdd(log);
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }
                    if (User.IsInRole("1"))
                    {
                        foreach (var item in results.Errors)
                        {
                            var log = new Log
                            {
                                Email = (string)Session["AdminUserName"],
                                Message = "ADMIN FAILED TO UPDATE ANNUAL LEAVE : Doctor Name: " + doctorManager.GetByID(annualLeave.doctorID).doctorPreName + " " + doctorManager.GetByID(annualLeave.doctorID).doctorLastName,
                                CreatedDate = DateTime.Now
                            };
                            logManager.LogAdd(log);
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }
                
            }

            return View();
        }
    }
}