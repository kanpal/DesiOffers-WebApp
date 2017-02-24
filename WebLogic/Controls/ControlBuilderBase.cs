using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebLogic.Controls
{
    public abstract class ControlBuilderBase<TViewControl, TBuilder> : IHtmlString
        where TViewControl : ControlBase
        where TBuilder : ControlBuilderBase<TViewControl, TBuilder>
    {        
        public ControlBuilderBase(TViewControl control)
        {
            Control = control;
        }

        protected internal TViewControl Control { get; set; }

        public virtual string ToHtmlString()
        {
            return string.Empty;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public TViewControl ToComponent()
        {
            return Control;
        }
    }
}
