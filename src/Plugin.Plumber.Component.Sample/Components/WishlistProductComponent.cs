using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Sample.Components
{
    public class WishlistProductComponent : Sitecore.Commerce.Core.Component
    { 
        public WishlistProductComponent()
        {
            this.ItemId = string.Empty;
        }

        public string ItemId { get; internal set; }
    }
}
