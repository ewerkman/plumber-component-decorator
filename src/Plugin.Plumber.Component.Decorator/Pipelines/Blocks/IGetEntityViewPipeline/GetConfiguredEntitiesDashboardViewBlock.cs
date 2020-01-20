namespace Sitecore.Commerce.Plugin.Catalog
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using global::Plugin.Plumber.Component.Decorator.Attributes;
    using global::Plugin.Plumber.Component.Decorator.Attributes.Dashboard;
    using global::Plugin.Plumber.Component.Decorator.Commanders;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using static global::Plugin.Plumber.Component.Constants;

    [PipelineDisplayName("GetConfiguredEntitiesDashboardViewBlock")]
    public class GetConfiguredEntitiesDashboardViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        protected EntityViewCommander Commander { get; set; }

        public GetConfiguredEntitiesDashboardViewBlock(EntityViewCommander commander)
            : base(null)
        {
            this.Commander = commander;
        }

        /// <summary>
        /// Executes the block
        /// </summary>
        /// <param name="arg">The entity view</param>
        /// <param name="context">The context</param>
        /// <returns></returns>
        public override async Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument cannot be null.");

            var request = context.CommerceContext.GetObjects<EntityViewArgument>().FirstOrDefault();

            if (IsDashboardView(request))
            {
                var requestedEntityName = request.ViewName.Substring(0, request.ViewName.IndexOf("-Dashboard"));
                var entities = this.Commander.GetAllEntityTypes();
                if (entities.Any())
                {
                    foreach (var entity in entities)
                    {
                        if (entity.Name == requestedEntityName)
                        {
                            var dashboardAttr = (NavigationAttributeAttribute)Attribute.GetCustomAttribute(entity, typeof(NavigationAttributeAttribute));

                            if (dashboardAttr != null)
                            {
                                arg.Icon = dashboardAttr.Icon.ToString();

                                // Add dashboard views
                                var dashboardViewAttrs = Attribute.GetCustomAttributes(entity, typeof(DashboardViewAttribute));
                                foreach (var attr in dashboardViewAttrs)
                                {
                                    var viewAttr = (DashboardViewAttribute)attr;
                                    var view = new EntityView()
                                    {
                                        Name = viewAttr.ViewName,
                                        Icon = viewAttr.Icon.ToString()
                                    };

                                    arg.ChildViews.Add(view);


                                }
                                break;
                            }
                        }
                    }
                }
                return arg;
            }

            return await Task.FromResult(arg).ConfigureAwait(false);
        }

        private bool IsDashboardView(EntityViewArgument request)
        {
            return !string.IsNullOrEmpty(request?.ViewName) && request.ViewName.EndsWith("-Dashboard");
        }
    }
}