namespace Plugin.Plumber.Component.Sample
{
    using Microsoft.Extensions.DependencyInjection;
    using Plugin.Plumber.Component.Sample.Components;
    using Plugin.Plumber.Component.Sample.Entities;
    using Plugin.Plumber.Component.Sample.Pipelines.IInitializeEnvironmentPipeline;
    using Plugin.Plumber.Component.Sample.ViewBuilders;
    using Sitecore.Commerce.Core;  
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

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
            services.Plumber().ViewComponents(config =>
                config.AddEntityView<Wishlist>()
                .AddViewComponent<WarrantyComponent>()
                .AddViewComponent<NotesComponent>()
                .AddViewComponent<SampleComponent>()
                .AddEntityViewBuilder<Wishlist, WishlistViewBuilder>());

            services.Sitecore().Pipelines(config =>
                    config.ConfigurePipeline<IInitializeEnvironmentPipeline>(c =>
                       c.Add<InitializeEnvironmentBlock>().After<IValidateEnvironmentPipeline>()
                    ));
            ;

        }
    }
}