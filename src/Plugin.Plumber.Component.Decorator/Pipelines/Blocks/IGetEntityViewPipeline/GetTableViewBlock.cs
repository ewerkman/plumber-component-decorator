namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IGetEntityViewPipeline
{
    using Plugin.Plumber.Component.Decorator.Attributes.Dashboard;
    using Plugin.Plumber.Component.Decorator.Commanders;
    using Plugin.Plumber.Component.Decorator.Extensions;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Plugin.Carts;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static Plugin.Plumber.Component.Constants;

    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class GetTableViewBlock : GetListViewBlock
    {
        protected EntityViewCommander EntityViewCommander { get; set; }

        public GetTableViewBlock(EntityViewCommander commander) : base(commander)
        {
            this.EntityViewCommander = commander;
        }       

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument can not be null");
            
            var request = context.CommerceContext.GetObjects<EntityViewArgument>().FirstOrDefault();

            if (!string.IsNullOrEmpty(request?.ViewName) && request.ViewName.EndsWith("-Dashboard"))
            {
                var requestedEntityName = request.ViewName.Substring(0, request.ViewName.IndexOf("-Dashboard"));
                var configuredEntities = this.EntityViewCommander.GetAllEntityTypes();
                if (configuredEntities.Any())
                {
                    foreach (var entity in configuredEntities)
                    {
                        if (entity.Name == requestedEntityName)
                        {
                            var tableViewAttr = (TableViewAttribute)Attribute.GetCustomAttribute(entity, typeof(TableViewAttribute));

                            if (tableViewAttr != null)
                            {
                                entityView.Icon = tableViewAttr.Icon.ToString();

                                // Add dashboard views
                                var tableViewAttrs = Attribute.GetCustomAttributes(entity, typeof(TableViewAttribute));
                                foreach (var attr in tableViewAttrs)
                                {
                                    var viewAttr = (TableViewAttribute)attr;
                                    var tableView = new EntityView()
                                    {
                                        Name = viewAttr.ViewName,
                                        UiHint = UIHints.Table,
                                        Icon = viewAttr.Icon.ToString()
                                    };

                                    entityView.ChildViews.Add(tableView);

                                    await this.SetListMetadata(tableView, viewAttr.ListName, $"Paginate-{entity.Name}-ViewTable", context);

                                    var entities = (await this.GetEntities(entityView, viewAttr.ListName, context)).Where(ent => ent.GetType() == entity);

                                    tableView.FillWithEntities(entities, viewAttr);

                                }
                                break;
                            }
                        }
                    }
                }
                return entityView;
            }

            return await Task.FromResult(entityView).ConfigureAwait(false);
            /*
            var cartViewsPolicy = context.GetPolicy<KnownCartViewsPolicy>();

            if (!arg.Name.Equals(cartViewsPolicy.CartsDashboard, StringComparison.InvariantCultureIgnoreCase))
            {
                return arg;
            }

            EntityViewArgument entityViewArgument = context.CommerceContext.GetObjects<EntityViewArgument>().FirstOrDefault<EntityViewArgument>();

            var cartsView = new EntityView()
            {
                Name = "Wishlists",
                DisplayName = "Wishlists",
                UiHint = "Table"
            };
            entityView.ChildViews.Add(cartsView);

            string listName = "Wishlists";
            await this.SetListMetadata(cartsView, listName, "PaginateCartsViewList", context);

            var carts = (await this.GetEntities(arg, "Carts", context)).OfType<Wishlist>();

            this.FillWithCarts(carts);

            return arg;
            */
        }
    }
}