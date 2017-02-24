using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Security.Claims;

namespace WebLogic.Security
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        protected string _claim;
        public string Claim
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_claim) && Access != null)
                {
                    _claim = Area != null ? Area.ToLower() + "." : string.Empty;
                    _claim += Access.ToLower();
                }
                return _claim;
            }
            set
            {
                _claim = value;
            }
        }
        public string Area { get; set; }
        public string Access { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
                return false;

            if (string.IsNullOrWhiteSpace(Claim))
                return true;

            //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = Request.LogonUserIdentity;

            if (!ActiveUser.GetActiveUser().HasClaim(CustomClaimTypes.Permission, Claim))
            //string userName = httpContext.User.Identity.Name.ToString();
            //ClaimsIdentity claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            //if (claimsIdentity == null || !claimsIdentity.HasClaim(CustomClaimTypes.Permission, Claim))
                return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {            
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "UnAuthorized",
                    requestedUrl = filterContext.RequestContext.HttpContext.Request.Url
                }));

            //base.HandleUnauthorizedRequest(filterContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}
