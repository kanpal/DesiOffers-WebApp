using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Web;

namespace WebLogic.Framework
{
    public static class Container
    {
        private static IUnityContainer _unityContainer;
        public static IUnityContainer DI
        {
            get
            {
                return _unityContainer;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["UnityContainer"] = value;
                }

                _unityContainer = value;
            }
        }

        public static T Resolve<T>() where T : class
        {
            IUnityContainer container = DI;
            if (container != null)
                return container.Resolve<T>();
            return null;
        }

        public static T Resolve<T>(string name) where T : class
        {
            IUnityContainer container = DI;
            if (container != null)
                return container.Resolve<T>(name);
            return null;
        }

        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            IUnityContainer container = DI;
            if (container != null)
                return container.ResolveAll<T>();
            return null;
        }

        public static bool IsRegistered<T>() where T : class
        {
            IUnityContainer container = DI;
            if (container != null)
                return container.IsRegistered<T>();
            return false;
        }

        public static bool IsRegistered<T>(string name) where T : class
        {
            IUnityContainer container = DI;
            if (container != null)
                return (container.IsRegistered<T>(name));
            return false;
        }
    }
}
