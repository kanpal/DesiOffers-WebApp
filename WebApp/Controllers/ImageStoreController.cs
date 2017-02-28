using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogic.Services;
using WebLogic.ViewModels;

namespace WebApp.Controllers
{
    public class ImageStoreController : Controller
    {
        // GET: ImageStore
        public ActionResult Get(long? id)
        {
            ImageService service = GeneralService.GetImageService();
            string imagePath = service.GetImagePath(id);
            if (System.IO.File.Exists(imagePath))
                return File(imagePath, service.GetContentType(imagePath));
            return new EmptyResult();
        }

        public ActionResult GetProduct(long? id)
        {
            ImageService service = GeneralService.GetImageService();
            string imagePath = service.GetImagePath(id);
            if (System.IO.File.Exists(imagePath))
                return File(imagePath, service.GetContentType(imagePath));
            imagePath = service.GetProductStockImagePath();
            if (System.IO.File.Exists(imagePath))
                return File(imagePath, service.GetContentType(imagePath));
            return new EmptyResult();
        }

        public ActionResult GetCustomer(long? id)
        {
            ImageService service = GeneralService.GetImageService();
            string imagePath = service.GetImagePath(id);
            if (System.IO.File.Exists(imagePath))
                return File(imagePath, service.GetContentType(imagePath));
            imagePath = service.GetCustomerStockImagePath();
            if (System.IO.File.Exists(imagePath))
                return File(imagePath, service.GetContentType(imagePath));
            return new EmptyResult();
        }

        public ActionResult AddProduct(long id, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                ImageService service = GeneralService.GetImageService();
                service.AddProductImage(id, imageFile);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid file selected.";
            }
            return RedirectToAction("Edit", "Product", new { id = id });
        }
    }
}