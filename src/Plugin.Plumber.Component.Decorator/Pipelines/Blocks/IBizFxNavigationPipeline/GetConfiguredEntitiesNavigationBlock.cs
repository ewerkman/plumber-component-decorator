namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IBizFxNavigationPipeline
{
    using Plugin.Plumber.Component.Decorator.Commanders;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Plugin.Plumber.Component.Decorator.Attributes.Dashboard;

    [PipelineDisplayName("Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IBizFxNavigationPipeline.GetConfiguredEntitiesNavigationBlock")]
    public class GetConfiguredEntitiesNavigationBlock : PipelineBlock<Sitecore.Commerce.EntityViews.EntityView, Sitecore.Commerce.EntityViews.EntityView, CommercePipelineExecutionContext>
    {
        protected EntityViewCommander Commander { get;}

        public GetConfiguredEntitiesNavigationBlock(EntityViewCommander commander) : base(null)
        {
            this.Commander = commander;
        }

        public override Task<Sitecore.Commerce.EntityViews.EntityView> Run(Sitecore.Commerce.EntityViews.EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{Name}: The argument cannot be null");

            var entities = this.Commander.GetAllEntityTypes();
            if(entities.Any())
            {
                AddConfiguredEntityTypesToNavigationView(entities, entityView, context);
            }
            
            return Task.FromResult(entityView);
        }

        private void AddConfiguredEntityTypesToNavigationView(List<Type> entities, EntityView entityView, CommercePipelineExecutionContext context)
        {
            foreach(var entity in entities)
            {
                var navigationAttr = (NavigationAttributeAttribute) Attribute.GetCustomAttribute(entity, typeof(NavigationAttributeAttribute));

                if (navigationAttr != null)
                {
                    entityView.ChildViews.Add(
                         new EntityView
                         {
                             Name = navigationAttr.DashboardName,
                             ItemId = navigationAttr.DashboardName+"-Dashboard",
                             Icon = navigationAttr.Icon.ToString(),
                             DisplayRank = navigationAttr.DisplayRank
                         });
                }
            }

 

        }
    }
}
