using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogic.Framework;
using WebLogic.Interfaces;
using WebLogic.Security;

namespace WebApp
{
    public class UnityFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _unityContainer;

        public UnityFactory()
        {
            _unityContainer = new UnityContainer();
        }

        public void RegisterContainer()
        {
            _unityContainer.RegisterType<IPrincipalBase, HttpPrincipal>();
            _unityContainer.RegisterType<ISessionCache, HttpSessionCache>();

            Container.DI = _unityContainer;
        }
    }
}