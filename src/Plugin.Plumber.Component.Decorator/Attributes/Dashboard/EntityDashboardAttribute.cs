using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Attributes.Dashboard
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class NavigationAttributeAttribute : System.Attribute
    {

        public string DashboardName { get; }
        public Icons Icon { get; }
        public int DisplayRank { get; }

        public NavigationAttributeAttribute(string dashboardName)
        {
            this.DashboardName = dashboardName;
        }

        public NavigationAttributeAttribute(string dashboardName, Icons icon = Icons.question, int displayRank = int.MaxValue)
        {
            this.DashboardName = dashboardName;
            this.Icon = icon;
            this.DisplayRank = displayRank;
        }
    }
}
