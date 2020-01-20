// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Acme.Plugin.Commerce.Index
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;
    using System.Reflection;
    using Acme.Plugin.Commerce.Index.Pipelines.Blocks;
    using Sitecore.Commerce.Plugin.Search;

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
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            // Configure pipelines
            // services.Sitecore().Pipelines(config => config

            //  .AddPipeline<ISamplePipeline, SamplePipeline>(
            //         configure =>
            //             {
            //                 configure.Add<SampleBlock>();
            //             })

            //    .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>()));

            services.Sitecore().Pipelines(config =>
                    config.ConfigurePipeline<IFullIndexMinionPipeline>(c =>
                       c.Add<InitializeOrdersIndexingViewBlock>().After<Sitecore.Commerce.Plugin.Orders.InitializeOrdersIndexingViewBlock>()
                    ));

            services.Sitecore().Pipelines(config =>
                    config.ConfigurePipeline<IIncrementalIndexMinionPipeline>(c =>
                       c.Add<InitializeOrdersIndexingViewBlock>().After<Sitecore.Commerce.Plugin.Orders.InitializeOrdersIndexingViewBlock>()
                    ));



            services.RegisterAllCommands(assembly);
        }
    }
}