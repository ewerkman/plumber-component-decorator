using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Plumber.Component.Decorator.Commanders;
using Plugin.Plumber.Component.Decorator.Extensions;

namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks
{
    /// <summary>
    ///     Populates the actions for the specified component
    /// </summary>
    [PipelineDisplayName("PopulateComponentActionsBlock")]
    public class PopulateComponentActionsBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly ComponentViewCommander componentViewCommander;        

        public PopulateComponentActionsBlock(ComponentViewCommander componentViewCommander)
        {
            this.componentViewCommander = componentViewCommander;
        }

        public async override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            var request = this.componentViewCommander.CurrentEntityViewArgument(context.CommerceContext);

            var commerceEntity = request?.Entity;

            if (commerceEntity != null)
            {
                List<Type> applicableComponentTypes = this.componentViewCommander.GetApplicableComponentTypes(commerceEntity, request.ItemId, context.CommerceContext);

                var editableComponentType = applicableComponentTypes.SingleOrDefault(type => type.FullName == arg.Name);

                if(editableComponentType != null)
                {
                    var entityViewAttribute = editableComponentType.GetEntityViewAttribute();

                    var actionPolicy = arg.GetPolicy<ActionsPolicy>();

                    actionPolicy.Actions.Add(new EntityActionView
                    {
                        Name = $"Edit-{editableComponentType.FullName}",
                        DisplayName = $"Edit {entityViewAttribute.ViewName}",
                        Description = entityViewAttribute.ViewName,
                        IsEnabled = true,
                        EntityView = arg.Name,
                        Icon = "edit"
                    });
                }
            }
            
            return await Task.FromResult(arg);
        }
    }
}
