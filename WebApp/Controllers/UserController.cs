using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Models;
using WebLogic.ViewModels;
using Microsoft.AspNet.Identity;
using MVC.Controls.Grid;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Roles
        public ActionResult Roles()
        {
            return View();
        }

        public JsonResult ListUsers() //int page, int rows, string sidx, string sort)
        {
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            List<UserViewModel> userModels = new List<UserViewModel>();
            foreach (var user in userManager.Users)
            {
                userModels.Add(new UserViewModel
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    Email = user.Email,
                });
            }
            return Json(userModels, JsonRequestBehavior.AllowGet);
            //var model = userModels.AsQueryable(); //.OrderBy(sidx + " " + sord);
            //return Json(model.ToGridData(page, rows, null, "", new[] { "Id", "UserName", "Email", "PhoneNumber" }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListRoles()
        {
            var roles = ApplicationRoleManger.ListAllRoles(new ApplicationDbContext());
            List<RoleViewModel> roleModels = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                roleModels.Add(new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            return Json(roleModels, JsonRequestBehavior.AllowGet);
        }
    }
}