using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.Authentication
{
    /// <summary>
    /// WebUserManager
    /// </summary>
    public class WebUserManager : UserManager<WebUser>
    {
        public WebUserManager(IUserStore<WebUser> store)
            : base(store)
        {
        }

        public static WebUserManager Create(IdentityFactoryOptions<WebUserManager> options, IOwinContext context)
        {
            var manager = new WebUserManager(new WebUserStore(context.Get<DesiOfferEntities>()));
            // Configure validation logic for usernames
            manager.UserLockoutEnabledByDefault = true;            
            return manager;
        }
    }

    /// <summary>
    /// WebSignInManager
    ///     Configure web sign-in manager for customers
    /// </summary>    
    public class WebSignInManager : SignInManager<WebUser, string>
    {
        public WebSignInManager(WebUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        //{
/*
            WebUser user = await UserManager.FindAsync(userName, password);
            if (user == null)
                return SignInStatus.Failure;
            return SignInStatus.Success;
            */
            //return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        //}

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(WebUser user)
        {
            return user.GenerateUserIdentityAsync((WebUserManager)UserManager);
        }

        public static WebSignInManager Create(IdentityFactoryOptions<WebSignInManager> options, IOwinContext context)
        {
            return new WebSignInManager(context.GetUserManager<WebUserManager>(), context.Authentication);
        }
    }

}
