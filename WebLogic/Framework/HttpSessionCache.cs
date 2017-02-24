using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using WebLogic.Interfaces;

namespace WebLogic.Framework
{
    public class HttpSessionCache : ISessionCache
    {
        public HttpSessionCache()
        {
        }

        protected HttpSessionState Session
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Session : null; }
        }

        public void RemoveData(string key)
        {
            if (Session != null)
                Session.Remove(key);
        }

        public object this[string key]
        {
            get { return RetrieveData(key); }
            set { SaveData(key, value); }
        }

        public object RetrieveData(string key)
        {
            return Session != null ? Session[key] : null;
        }

        public void SaveData(string key, object value)
        {
            if (Session != null)
                Session[key] = value;
        }

        public void Clear()
        {
            Session.Clear();
        }
    }
}
