using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Plugin.Plumber.Component.Constants;

namespace Plugin.Plumber.Component.Decorator.Attributes.Dashboard
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TableViewAttribute : System.Attribute
    {
        public string ViewName { get; }
        public string ListName { get; }
        public Icons Icon { get; }
        public string UIHint { get; }

        /// <summary>
        ///     Pipe (|) delimited string of properties to display
        /// </summary>
        public string DisplayProperties { get; }    

        public TableViewAttribute(string viewName, string listName, Icons icon = Icons.question, string uiHint = UIHints.Table, string displayProperties = null) : base()
        {
            this.ViewName = viewName;
            this.ListName = listName;
            this.Icon = icon;
            this.UIHint = uiHint;
            this.DisplayProperties = displayProperties;
        }
    }
}
