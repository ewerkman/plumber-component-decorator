// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoActionPaginateListViewBlock.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2019
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IDoActionPipeline
{
    using Plugin.Plumber.Component.Decorator.Attributes.Dashboard;
    using Plugin.Plumber.Component.Decorator.Commanders;
    using Plugin.Plumber.Component.Decorator.Extensions;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Plugin.Plumber.Component.Constants;

    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class DoActionPaginateTableViewBlock : DoActionPaginateListBlock
    {
        protected EntityViewCommander EntityViewCommander { get; set; }

        public DoActionPaginateTableViewBlock(EntityViewCommander commander)
            : base(commander)
        {
            this.EntityViewCommander = commander;
        }

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument can not be null");

            if (!(entityView.Action.StartsWith("Paginate-") || entityView.Action.EndsWith("-ViewTable")) || !this.Validate(entityView, context.CommerceContext))
            {
                return entityView;
            }

            var actionParts = entityView.Action.Split('-');
            if (actionParts.Length == 3)
            {
                var requestedEntityName = actionParts[1];
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
                                var allEntities = await this.GetEntities(entityView, context).ConfigureAwait(false);
                                var entities = allEntities.Where(ent => ent.GetType() == entity);

                                entityView.FillWithEntities(entities, tableViewAttr);
                            }
                            break;
                        }
                    }
                }
            }

            return entityView;
        }
    }
}