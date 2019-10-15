using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PropertyAttribute : System.Attribute
    {
        public string DisplayName { get; private set; }
        public string UIType { get; private set; }
        public bool IsReadOnly { get; private set; }
        public bool IsRequired { get; private set; }
        public bool ShowInList { get; private set; }
        public bool Localize { get; private set; }
 
        public PropertyAttribute(string displayName = "", 
            string UIType = null,
            bool isReadOnly = false, 
            bool isRequired = false,
            bool showInList = true,
            bool localize = false)
        {
            this.DisplayName = displayName;
            this.UIType = UIType;
            this.IsReadOnly = isReadOnly;
            this.IsRequired = isRequired;
            this.ShowInList = showInList;
            this.Localize = localize;
        }
    }
}
