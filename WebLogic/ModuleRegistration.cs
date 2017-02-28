using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic
{
    public static class ModuleRegistration
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<DesiOfferEntities>(CreateDesiOfferEntities);
        }

        public static DesiOfferEntities CreateDesiOfferEntities(IdentityFactoryOptions<DesiOfferEntities> options, IOwinContext context)
        {
            return new DesiOfferEntities();
        }
    }
}
