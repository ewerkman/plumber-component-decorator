using Microsoft.Extensions.DependencyInjection;
using Plugin.Plumber.Component.Decorator.Pipelines.Blocks.IBizFxNavigationPipeline;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.BusinessUsers;
using Sitecore.Commerce.Plugin.Composer;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using System.Reflection;

namespace Plugin.Plumber.Component
{
    /// <summary>
    /// The configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.Sitecore()
                .ConfigureGetEntityViewPipeline()
                .ConfigurePopulateEntityViewActionsPipeline()
                .ConfigureDoActionPipeline()
                .AddGetApplicableViewConditionsPipeline()
                .Pipelines(config =>
                        config.ConfigurePipeline<IRunningPluginsPipeline>(c =>
                            c.Add<Decorator.Pipelines.Blocks.RegisteredPluginBlock>())
                        );

            services.Sitecore().Pipelines(config =>
                    config.ConfigurePipeline<IBizFxNavigationPipeline>(c =>
                       c.Add<GetConfiguredEntitiesNavigationBlock>().After<GetComposerNavigationViewBlock>()
                    ));

        }
    }
}