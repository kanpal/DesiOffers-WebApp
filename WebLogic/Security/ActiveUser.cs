using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Framework;
using WebLogic.Interfaces;

namespace WebLogic.Security
{
    public class ActiveUser
    {
        public static bool StripDomainNameFromUserName = true;

        public bool IsAuthenticated
        {
            get
            {
                var principal = Container.Resolve<IPrincipalBase>();
                return principal.IsAuthenticated;
            }
        }

        public string UserName
        {
            get
            {
                return ActiveUser.GetActiveUserName();
            }
        }

        public string GetClaim(string claimType)
        {
            var principal = Container.Resolve<IPrincipalBase>();
            HttpPrincipal httpPrincipal = principal as HttpPrincipal;
            if (httpPrincipal == null)
                return null;
            return httpPrincipal.GetClaim(claimType);
        }

        public bool HasClaim(string claimType, string value)
        {
            var principal = Container.Resolve<IPrincipalBase>();
            HttpPrincipal httpPrincipal = principal as HttpPrincipal;
            if (httpPrincipal == null)
                return false;
            return httpPrincipal.HasClaim(claimType, value);
        }

        /// <summary>
        /// Light-weight static method: Gets the current thread principal's login name
        /// </summary>
        /// <returns>User's login/user name</returns>
        public static string GetActiveUserName()
        {
            var principal = Container.Resolve<IPrincipalBase>();
            if (principal.IsAuthenticated)
            {
                string userName = principal.Name;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    if (StripDomainNameFromUserName)
                    {
                        int domainLoc = userName.IndexOf(@"\");
                        if (domainLoc > 0)
                            userName = userName.Substring(domainLoc + 1);
                    }
                    return userName;
                }
            }
            return null;
        }

        public static ActiveUser GetActiveUser()
        {
            ActiveUser activeUser = null;

            var sessionCache = Container.Resolve<ISessionCache>();
            activeUser = sessionCache["ActiveUser"] as ActiveUser;

            if (activeUser == null)
            {
                activeUser = new ActiveUser();
                sessionCache["ActiveUser"] = activeUser;
            }

            return activeUser;
        }
    }
}
