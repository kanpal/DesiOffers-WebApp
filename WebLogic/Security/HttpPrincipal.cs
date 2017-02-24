using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebLogic.Interfaces;

namespace WebLogic.Security
{
    public class HttpPrincipal : IPrincipalBase
    {
        public HttpPrincipal()
        {
        }

        protected IPrincipal Principal
        {
            get { return HttpContext.Current != null ? HttpContext.Current.User : null; }
        }

        public bool IsAuthenticated
        {
            get
            {
                if (Principal != null)
                    return Principal.Identity.IsAuthenticated;
                return false;
            }
        }

        public string Name
        {
            get
            {
                if (Principal != null)
                {
                    if (!string.IsNullOrEmpty(Principal.Identity.Name))
                        return Principal.Identity.Name;

                    var identity = Principal.Identity;

                    var claimsIdentity = identity as ClaimsIdentity;
                    if (identity is ClaimsIdentity)
                        return GetClaim(ClaimTypes.Name);
                }
                return "";
            }
        }

        public string GetClaim(string claimType)
        {
            if (Principal == null)
                return null;

            var claimsIdentity = Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;

            return (from c in claimsIdentity.Claims where c.Type == claimType select c.Value).FirstOrDefault();
        }

        public bool HasClaim(string claimType, string value)
        {
            if (Principal == null)
                return false;

            var claimsIdentity = Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return false;
            return claimsIdentity.HasClaim(claimType, value);
        }
    }
}
