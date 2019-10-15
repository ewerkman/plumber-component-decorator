namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks
{
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System.Linq;
    using System.Threading.Tasks;

    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class InitializePlumberLocalization : PipelineBlock<Sitecore.Commerce.Core.CommerceEnvironment, Sitecore.Commerce.Core.CommerceEnvironment, CommercePipelineExecutionContext>
    {
        protected CommerceCommander Commander { get; set; }

        public InitializePlumberLocalization(CommerceCommander commander)
            : base(null)
        {
            this.Commander = commander;
        }

        public async override Task<Sitecore.Commerce.Core.CommerceEnvironment> Run(Sitecore.Commerce.Core.CommerceEnvironment arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument can not be null");

            /* Add business logic here */
            var policies = arg.GetPolicies<PolicySetPolicy>().Where(p => p.PolicySetId == "Entity-PolicySet-LocalizeEntitiesPolicySet").FirstOrDefault();

            var policySet = await Commander.GetEntity<PolicySet>(context.CommerceContext, policies.PolicySetId);
            

            return arg;
        }
    }
}
