using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        [Authorize]
        public ActionResult Index()
        {
            LogManager logManager = new LogManager(new EfLogDal());
            using (Context context = new Context())
            {

                var logs = context.Logs.OrderByDescending(l => l.CreatedDate).ToList();
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE LOGS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return View(logs);
            }
        }
    }
}