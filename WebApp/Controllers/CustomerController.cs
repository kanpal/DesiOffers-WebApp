using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogic.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return View(customerService.Get(id));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult List(string name)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return Json(customerService.ListMatching(name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListTransactions(long id)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return Json(customerService.ListTransactions(id), JsonRequestBehavior.AllowGet);
        }
    }
}
