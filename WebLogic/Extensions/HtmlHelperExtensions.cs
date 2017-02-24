using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebLogic.Controls;

namespace WebLogic.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static JSGrid<T> JSGrid<T>(this HtmlHelper helper)
        {
            return new JSGrid<T>(helper);
        }
    }
}
