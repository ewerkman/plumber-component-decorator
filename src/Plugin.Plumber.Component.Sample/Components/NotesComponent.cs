using System.ComponentModel.DataAnnotations;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Inventory;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Promotions;
using Plugin.Plumber.Component.Decorator.Attributes;

namespace Plugin.Plumber.Component.Sample.Components
{
    [AddToEntityType(typeof(SellableItem))]
    [AddToEntityType(typeof(InventoryInformation))]
    [AddToEntityType(typeof(Promotion))]
    [AddToEntityType(typeof(Order))]
    [AddToAllEntityTypes]
    [EntityView("Notes")]
    public class NotesComponent : Sitecore.Commerce.Core.Component
    {
        [Property("External Notes", isRequired: true, showInList: true, localize:true)]
        public string ExternalNotes { get; set; } = string.Empty;

        [Property("Internal Notes", UIType:"RichText")]
        [Required]
        public string InternalNotes { get; set; } = string.Empty;
    }
}
