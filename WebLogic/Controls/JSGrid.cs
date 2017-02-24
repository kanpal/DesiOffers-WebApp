using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebLogic.Controls
{
    /// <summary>
    /// JSGrid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JSGrid<T> : ControlBase
    {
        public JSGrid(ViewContext viewContext, ViewDataDictionary viewData = null) : base(viewContext, viewData)
        {
        }

        public JSGrid(HtmlHelper htmlHelper) : base (htmlHelper.ViewContext, htmlHelper.ViewDataContainer != null ? htmlHelper.ViewDataContainer.ViewData : null)
        {            
        }
    }

    /// <summary>
    /// JSGridBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JSGridBuilder<T> : ControlBuilderBase<JSGrid<T>, JSGridBuilder<T>> where T : class
    {
        public JSGridBuilder(JSGrid<T> control) : base(control)
        {
        }
    }
}
