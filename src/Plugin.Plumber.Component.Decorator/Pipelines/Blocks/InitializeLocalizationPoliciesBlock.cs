namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks
{
    using Plugin.Plumber.Component.Decorator.Attributes;
    using Plugin.Plumber.Component.Decorator.Commanders;
    using Plugin.Plumber.Component.Decorator.Extensions;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Plugin.Catalog;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a pipeline block
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         Sitecore.Framework.Pipelines.PipelineBlock{Sitecore.Commerce.Core.PipelineArgument,
    ///         Sitecore.Commerce.Core.PipelineArgument, Sitecore.Commerce.Core.CommercePipelineExecutionContext}
    ///     </cref>
    /// </seealso>
    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class InitializeLocalizationPoliciesBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        protected ComponentViewCommander Commander { get; set; }

        public InitializeLocalizationPoliciesBlock(ComponentViewCommander commander)
            : base(null)
        {
            this.Commander = commander;
        }

        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument can not be null");

            var request = this.Commander.CurrentEntityViewArgument(context.CommerceContext);

            var commerceEntity = request?.Entity;
                      
            if (commerceEntity != null)
            {
                var type = commerceEntity?.GetType();

                var isSellableItemVariant = !string.IsNullOrEmpty(arg.ItemId) && commerceEntity is SellableItem;
                var pathPrefix = !isSellableItemVariant ? string.Empty : "ItemVariationsComponent.ItemVariationComponent.";

                List<Type> applicableComponentTypes = this.Commander.GetApplicableComponentTypes(commerceEntity, request.ItemId, context.CommerceContext);

                foreach (var componentType in applicableComponentTypes)
                {
                    var componentProps = componentType.GetProperties().Where(prop => prop.GetCustomAttributes(true).Where(attr => attr is PropertyAttribute propAttr && propAttr.Localize).Any());
                    if (componentProps.Any())
                    {
                        ConfigureLocalization(arg, context, type, isSellableItemVariant, pathPrefix, componentType, componentProps);
                    }
                }
            }

            return Task.FromResult(arg);
        }

        private void ConfigureLocalization(EntityView entityView, CommercePipelineExecutionContext context, Type entityType, bool isSellableItemVariant, string pathPrefix, Type componentType, IEnumerable<System.Reflection.PropertyInfo> props)
        {
            var policy = context.CommerceContext.Environment.GetPolicies<LocalizeEntityPolicy>().FirstOrDefault(p => p.Type.Equals(entityType.FullName, StringComparison.OrdinalIgnoreCase));

            if (policy == null)  // There is no LocalizationEntityPolicy for this entity type
            {
                policy = new LocalizeEntityPolicy(entityType.FullName, Enumerable.Empty<string>(), entityView.Name);
                context.CommerceContext.Environment.Policies.Add(policy);
            }

            var componentPolicy = !isSellableItemVariant ? policy.GetComponentPolicyByView(componentType.FullName) : policy.GetItemComponentPolicyByView(componentType.FullName);
            if (componentPolicy is null)
            {
                componentPolicy = new LocalizeEntityComponentPolicy(componentType.Name, props.Select(prop => prop.Name).ToArray(), componentType.FullName);

                var localizationPolicies = policy.ComponentsPolicies.ToList();
                localizationPolicies.Add(componentPolicy);
                policy.ComponentsPolicies = localizationPolicies;
            }
            componentPolicy.Properties = props.Select(prop => prop.Name).ToArray();
            componentPolicy.Path = pathPrefix + componentType.Name;
            componentPolicy.ActionView = componentType.FullName;
            componentPolicy.IsItemComponent = isSellableItemVariant;
        }
    }
}