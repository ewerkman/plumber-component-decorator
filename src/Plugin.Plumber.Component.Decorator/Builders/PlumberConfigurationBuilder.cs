using Microsoft.Extensions.DependencyInjection;
using Plugin.Plumber.Component.Decorator.Views;
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

        public IPlumberConfigurationBuilder AddEntityView<TEntity>() where TEntity : CommerceEntity
        {
            this.services.Add(new ServiceDescriptor(typeof(EntityViewDefinition), p => new EntityViewDefinition<TEntity>(), ServiceLifetime.Singleton));

            return this;
        }

        public IPlumberConfigurationBuilder AddViewComponent<TViewComponent>() where TViewComponent : Sitecore.Commerce.Core.Component
        {
            this.services.Add(new ServiceDescriptor(typeof(ViewComponentDefinition), 
                p => new ViewComponentDefinition<TViewComponent>(), 
                ServiceLifetime.Singleton));

            return this;
        }

        public IPlumberConfigurationBuilder AddCompnentViewBuilder<TComponent, TViewBuilder>()
            where TComponent : Sitecore.Commerce.Core.Component
            where TViewBuilder : IComponentViewBuilder
        {
            this.services.Add(new ServiceDescriptor(typeof(ComponentViewBuilderDefinition),
                p => new ComponentViewBuilderDefinition<TComponent, TViewBuilder>(),
                ServiceLifetime.Singleton));
            return this;
        }

        public IPlumberConfigurationBuilder AddEntityViewBuilder<TCommerceEntity, TViewBuilder>()
            where TCommerceEntity : CommerceEntity
            where TViewBuilder : ICommerceEntityViewBuilder
        {
            this.services.Add(new ServiceDescriptor(typeof(ComponentViewBuilderDefinition),
               p => new CommerceEntityViewBuilderDefinition<TCommerceEntity, TViewBuilder>(),
               ServiceLifetime.Singleton));
            return this;
        }
    }
}
