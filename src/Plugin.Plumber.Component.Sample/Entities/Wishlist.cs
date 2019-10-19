namespace Plugin.Plumber.Component.Sample.Entities
{
    using System;
    using System.Collections.Generic;
    using Plugin.Plumber.Component.Decorator.Attributes;
    using Sitecore.Commerce.Core;
    
    [EntityView("Wish list")]
    public class Wishlist : CommerceEntity
    {
        public Wishlist()
        {
            this.Components = new List<Component>();
            this.DateCreated = DateTime.UtcNow;
            this.DateUpdated = this.DateCreated;
        }

        public Wishlist(string id) : this()
        {
            this.Id = id;
        }
    }
}