using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using System.Collections.Generic;

namespace Plugin.Plumber.Component.Decorator.Builders
{
    public class PlumberConfigurationBuilder : IPlumberConfigurationBuilder
    {
        private readonly IServiceCollection services;
        private readonly List<System.Type> viewComponents = new List<System.Type>();

        public PlumberConfigurationBuilder(IServiceCollection services)
        {
            Condition.Requires<IServiceCollection>(services, nameof(services)).IsNotNull<IServiceCollection>();
            this.services = services;
        }

        public PlumberConfigurationBuilder AddViewComponent<TViewComponent>() where TViewComponent : Sitecore.Commerce.Core.Component
        {
            this.services.Add(new ServiceDescriptor(typeof(ViewComponentDefinition), p => new ViewComponentDefinition<TViewComponent>(), ServiceLifetime.Singleton));

            return this;
        }
    }
}
