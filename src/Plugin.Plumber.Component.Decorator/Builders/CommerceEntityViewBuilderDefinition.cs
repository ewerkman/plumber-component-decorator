using Plugin.Plumber.Component.Decorator.Views;
using Sitecore.Framework.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Builders
{
    public class CommerceEntityViewBuilderDefinition<TCommerceEntity, TViewBuilder> : ComponentViewBuilderDefinition
        where TCommerceEntity : Sitecore.Commerce.Core.CommerceEntity
        where TViewBuilder : ICommerceEntityViewBuilder
    {
        public CommerceEntityViewBuilderDefinition() : base(typeof(TCommerceEntity), typeof(TViewBuilder))
        {
        }
    }

    public abstract class CommerceEntityViewBuilderDefinition
    {
        public Type CommerceEntity { get; }
        public Type ViewBuilder { get; }

        public CommerceEntityViewBuilderDefinition(Type commerceEntity, Type viewBuilder)
        {
            Condition.Requires<Type>(commerceEntity, nameof(commerceEntity)).IsNotNull<Type>();
            Condition.Requires<Type>(viewBuilder, nameof(viewBuilder)).IsNotNull<Type>();

            CommerceEntity = commerceEntity;
            ViewBuilder = viewBuilder;
        }
    }
}
