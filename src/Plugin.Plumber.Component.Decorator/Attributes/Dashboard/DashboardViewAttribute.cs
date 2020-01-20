using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Plumber.Component.Constants;

namespace Plugin.Plumber.Component.Decorator.Attributes.Dashboard
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DashboardViewAttribute : System.Attribute
    {
        public string ViewName { get; }
        public Icons Icon { get; }
        public string UIHint { get; }

        public DashboardViewAttribute(string viewName, Icons icon = Icons.question, string uiHint = UIHints.Table)
        {
            this.ViewName = viewName;
            this.Icon = icon;
            this.UIHint = uiHint;
        }
    }
}
