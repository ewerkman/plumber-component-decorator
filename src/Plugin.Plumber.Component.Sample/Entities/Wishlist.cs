namespace Plugin.Plumber.Component.Sample.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.OData.Builder;
    using Plugin.Plumber.Component.Decorator;
    using Plugin.Plumber.Component.Decorator.Attributes.Dashboard;
    using Plugin.Plumber.Component.Sample.Components;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Carts;

    [NavigationAttribute("Wishlist", Icons.list_style_bullets, displayRank:3)]
    [DashboardView("Wish lists", Icons.list_style_roman)]
    [TableView("Wish Lists", "Wishlists", Icons.list_style_numbered, displayProperties:"Id|DisplayName|Name")]
    public class Wishlist : CommerceEntity
    {
        public Wishlist()
        {
            this.Components = new List<Component>();
            this.DateCreated = DateTime.UtcNow;
            this.DateUpdated = this.DateCreated;

            this.Lines = new List<WishlistLineComponent>();
        }

        public Wishlist(string id) : this()
        {
            this.Id = id;            
        }

        public bool IsFavorite { get; set; }
        public string CustomerId { get; set; }

        public int ItemCount => this.Lines.Count; 

        [Contained]
        public IList<WishlistLineComponent> Lines { get; set; }
    }
}