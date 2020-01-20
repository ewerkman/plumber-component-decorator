using Plugin.Plumber.Component.Decorator.Views;

namespace Plugin.Plumber.Component.Decorator.Builders
{
    public interface IPlumberConfigurationBuilder
    {
        IPlumberConfigurationBuilder AddEntityView<TEntity>() 
            where TEntity : Sitecore.Commerce.Core.CommerceEntity;

        IPlumberConfigurationBuilder AddViewComponent<TComponent>() 
            where TComponent: Sitecore.Commerce.Core.Component;

        IPlumberConfigurationBuilder AddCompnentViewBuilder<TComponent, TViewBuilder>()
            where TComponent : Sitecore.Commerce.Core.Component
            where TViewBuilder : IComponentViewBuilder;

        IPlumberConfigurationBuilder AddEntityViewBuilder<TCommerceEntity, TViewBuilder>()
           where TCommerceEntity : Sitecore.Commerce.Core.CommerceEntity
           where TViewBuilder : ICommerceEntityViewBuilder;
    }
}