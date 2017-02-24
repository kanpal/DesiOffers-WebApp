using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebLogic.Interfaces;

namespace WebLogic.Controls
{
    public abstract class ControlBase : IControl, IHtmlAttributesContainer
    {
        public string Id { get; }
        public string Name { get; set; }

        protected ViewContext _viewContext;
        public ViewContext ViewContext
        {
            get { return _viewContext; }
        }

        protected ViewDataDictionary _viewData;
        public ViewDataDictionary ViewData
        {
            get { return _viewData; }
        }

        public ModelMetadata ModelMetadata
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<string, object> HtmlAttributes
        {
            get { throw new NotImplementedException(); }
        }

        protected ControlBase(ViewContext viewContext, ViewDataDictionary viewData = null)
        {
            _viewContext = viewContext;
            _viewData = viewData;
        }

        public void Render()
        {
            // TODO
        }

        public string ToHtmlString()
        {
            return string.Empty;
        }
    }
}
