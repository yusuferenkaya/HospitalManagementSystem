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

namespace HospitalManagementSystem.Controllers
{
    public class AdminStockController : Controller
    {
        // GET: AdminCategory
        LogManager logManager = new LogManager(new EfLogDal());
        StockManager sm = new StockManager(new EfStockDal());
        StockValidator sv = new StockValidator();
        [Authorize]
        public ViewResult Index()
        {
            // Result needs to be IQueryable in database scenarios, to make use of database side paging.
            var stockvalues = sm.GetList();
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN DISPLAYED THE STOCKS", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(stockvalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddStock()
        {
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE ADDING A NEW STOCK PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddStock(Stock stock)
        {
            StockValidator stockValidator = new StockValidator();
            ValidationResult results = stockValidator.Validate(stock);

            if (results.IsValid)
            {
                sm.StockAdd(stock);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ADDED A NEW STOCK : Stock Name: " + stock.stockName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO ADD A NEW STOCK : Stock ID: " + stock.stockName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult DeleteStock(int id)
        {
            var stockvalue = sm.GetByID(id);
            sm.StockDelete(stockvalue);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN REMOVED THE STOCK : Stock Name: " + stockvalue.stockName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateStock(int id)
        {
            var stockvalue = sm.GetByID(id);
            var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN ENTERED THE UPDATING A STOCK PAGE", CreatedDate = DateTime.Now }; logManager.LogAdd(log);
            return View(stockvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateStock(Stock stock)
        {
            ValidationResult results = sv.Validate(stock);

            if (results.IsValid)
            {
                sm.StockUpdate(stock);
                var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN UPDATED THE STOCK : Stock Name: " + stock.stockName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                logManager.LogAdd(log);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    var log = new Log { Email = (string)Session["AdminUserName"], Message = "ADMIN FAILED TO UPDATE THE STOCK : Stock Name: " + stock.stockName, CreatedDate = DateTime.Now }; logManager.LogAdd(log);
                    logManager.LogAdd(log);
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View();

            
        }



    }
}