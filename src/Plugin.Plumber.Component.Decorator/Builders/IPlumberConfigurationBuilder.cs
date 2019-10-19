namespace Plugin.Plumber.Component.Decorator.Builders
{
    public interface IPlumberConfigurationBuilder
    {
        IPlumberConfigurationBuilder AddEntityView<TEntity>() where TEntity : Sitecore.Commerce.Core.CommerceEntity;
        IPlumberConfigurationBuilder AddViewComponent<TComponent>() where TComponent: Sitecore.Commerce.Core.Component;
    }
}