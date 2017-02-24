using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebLogic.Interfaces
{
    public interface IControl : IHtmlAttributesContainer
    {
        string Id { get; }
        ModelMetadata ModelMetadata { get; }
        string Name { get; }
        ViewContext ViewContext { get; }
        ViewDataDictionary ViewData { get; }
    }

    public interface IHtmlAttributesContainer
    {
        Dictionary<string, object> HtmlAttributes { get; }
    }
}
