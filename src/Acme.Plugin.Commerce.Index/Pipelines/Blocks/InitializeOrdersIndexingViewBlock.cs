namespace Acme.Plugin.Commerce.Index.Pipelines.Blocks
{
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Plugin.Orders;
    using Sitecore.Commerce.Plugin.Search;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class InitializeOrdersIndexingViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        protected CommerceCommander Commander { get; set; }

        public InitializeOrdersIndexingViewBlock(CommerceCommander commander)
            : base(null)
        {
            this.Commander = commander;
        }

        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: argument cannot be null.");

            var argument = context.CommerceContext.GetObjects<SearchIndexMinionArgument>().FirstOrDefault();
            if (string.IsNullOrEmpty(argument?.Policy?.Name))
            {
                return Task.FromResult(arg);
            }

            var orders = argument.Entities?.OfType<Order>().ToList();
            if (orders == null || !orders.Any())
            {
                return Task.FromResult(arg);
            }

            var searchViewNames = context.GetPolicy<KnownSearchViewsPolicy>();

            orders.ForEach(
              order =>
              {
                  var documentView = arg.ChildViews.Cast<EntityView>()
                    .FirstOrDefault(v => v.EntityId.Equals(order.Id, StringComparison.OrdinalIgnoreCase) && v.Name.Equals(searchViewNames.Document, StringComparison.OrdinalIgnoreCase));

                  if (documentView == null)
                  {
                      documentView = new EntityView
                      {
                          Name = context.GetPolicy<KnownSearchViewsPolicy>().Document,
                          EntityId = order.Id
                      };
                      arg.ChildViews.Add(documentView);
                  }

                  documentView.Properties.Add(new ViewProperty { Name = "total", RawValue = order.Totals.GrandTotal.Amount });

              });


            return Task.FromResult(arg);
        }
    }
}
