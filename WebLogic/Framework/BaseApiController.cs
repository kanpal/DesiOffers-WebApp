using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace WebLogic.Framework
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Uses JSON.NET for serializing, which correctly serializes Dates and other data
        /// </summary>
        protected JsonResult Json(object data)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
