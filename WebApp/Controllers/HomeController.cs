using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogic.Framework;
using WebLogic.Security;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles="Administrators")]
        [AuthorizeUser(Access = Permissions.SystemAdministration)]
        public ActionResult Admin()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "DesiOffers LLC.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact:";

            return View();
        }
        public ActionResult UnAuthorized(string message, string requestedUrl)
        {
            ViewBag.ReturnUrl = requestedUrl;
            ViewBag.Message = message;
            return View();
        }
    }
}