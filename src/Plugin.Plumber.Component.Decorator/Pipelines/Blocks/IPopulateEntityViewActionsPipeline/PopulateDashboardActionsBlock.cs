namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IPopulateEntityViewActionsPipeline
{
    using Plugin.Plumber.Component.Decorator.Commanders;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System.Threading.Tasks;

    public class PopulateDashboardActionsBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly ComponentViewCommander componentViewCommander;

        public PopulateDashboardActionsBlock(ComponentViewCommander componentViewCommander)
        {
            this.componentViewCommander = componentViewCommander;
        }
        public override async Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            var request = this.componentViewCommander.CurrentEntityViewArgument(context.CommerceContext);

            if (request != null)
            {
                var commerceEntity = request?.Entity;

                if (commerceEntity != null)
                {
                }
            }
            return await Task.FromResult(arg);
        }
    }
}
