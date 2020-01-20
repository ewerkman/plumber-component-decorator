using Plugin.Plumber.Component.Decorator.Attributes.Dashboard;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Extensions
{
    public static class EntityViewExtensions
    {
        public static EntityView FillWithEntities(this EntityView tableView, IEnumerable<CommerceEntity> entities, TableViewAttribute tableViewAttribute)
        {
            var displayProperties = tableViewAttribute.DisplayProperties?.Split('|');
            PropertyInfo[] properties = null;

            foreach (var entity in entities)
            {
                if (properties == null)
                {
                    properties = entity.GetType().GetProperties();
                }

                var rowView =
                      new EntityView
                      {
                          EntityId = entity.Id,
                          ItemId = entity.Id,
                          Icon = tableViewAttribute.Icon.ToString()
                      };

                if (displayProperties == null || displayProperties.Length == 0)
                {
                    rowView.Properties.AddRange(
                       new[]
                           {
                                new ViewProperty { Name = "Name", RawValue = entity.Name, IsReadOnly = true, UiType = "EntityLink" },
                                new ViewProperty { Name = "DisplayName", RawValue = entity.DisplayName, IsReadOnly = true }
                           });
                }
                else
                {
                    var firstProperty = true;
                    foreach (var displayProperty in displayProperties)
                    {
                        var property = properties.Where(prop => prop.Name == displayProperty);
                        if(property != null)
                        {
                            var propValue = entity.GetType().GetProperty(displayProperty).GetValue(entity);
                            rowView.Properties.Add(new ViewProperty {
                                Name = displayProperty,
                                RawValue = propValue,
                                IsReadOnly = true,
                                UiType = firstProperty ? "EntityLink" : ""
                            });

                            firstProperty = false;
                        }
                    }
                }

                tableView.ChildViews.Add(rowView);
            }

            return tableView;
        }
    }
}
