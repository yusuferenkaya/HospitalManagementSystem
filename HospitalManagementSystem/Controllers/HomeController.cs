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
    public class HomeController : Controller
    {
        // GET: Home

        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        public ActionResult Index()
        {
            var departmentvalues = dm.GetList();
            return View(departmentvalues);
        }
    }
}