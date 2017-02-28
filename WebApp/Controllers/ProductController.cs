using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogic.Security;
using WebLogic.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Categories = GeneralService.GetCategories();
            return View();
        }

        // GET: Product/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            ProductService productService = GeneralService.GetProductService();
            ActiveUser user = ActiveUser.GetActiveUser();
            if (user.IsAuthenticated)
            {
                var transaction = productService.GetLeadingOffer(id);
                if (transaction == null)
                    ViewBag.Message = "There are no offers yet!";
                else
                {
                    ViewBag.Message = (transaction.CustomerID == user.CustomerId) ? "You are leading this offer!" : "You are not leading!";
                }
            }
            return View(productService.Get(id));
        }

        // GET: Product/Create        
        public ActionResult Create()
        {
            ProductService productService = GeneralService.GetProductService();
            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductService productService = GeneralService.GetProductService();
            return View(productService.Get(id));
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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

        // POST: Product/MakeOffer
        [HttpPost]
        public ActionResult MakeOffer(int id, string offerPrice, long? customerId)
        {
            string message = string.Empty, newPrice = offerPrice;
            bool result = true;
            try
            {
                if ((customerId ?? 0) == 0)
                    customerId = ActiveUser.GetActiveUser().CustomerId;

                if (!customerId.HasValue || customerId.Value == 0)
                    throw new Exception("You are not authorized to submit an offer.");

                ProductService productService = GeneralService.GetProductService();
                Decimal price = 0;
                if (Decimal.TryParse(offerPrice, out price))
                {
                    productService.CreateOffer(id, price, customerId.Value);
                    message = string.Format("Offer for ${0:2} submitted.", offerPrice);
                    newPrice = price.ToString("C");
                }
                else
                {
                    message = string.Format("Offer price ${0} is invalid.", offerPrice);
                }                
            }
            catch (Exception ex)
            {
                message = string.Format("Error submitting the offer; {0}", ex.Message);
                result = false;
            }

            return Json(new { result = result, message = message, newPrice = newPrice });
            //return RedirectToAction("Details", new { id = id });
        }

        [AllowAnonymous]
        public JsonResult List(string name)
        {
            ProductService productService = GeneralService.GetProductService();
            return Json(productService.ListMatching(name, 200), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ListTransactions(long id)
        {
            ProductService productService = GeneralService.GetProductService();
            return Json(productService.ListTransactions(id), JsonRequestBehavior.AllowGet);
        }
    }
}
