using Plugin.Plumber.Component.Decorator.Views;
using Sitecore.Framework.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Builders
{
    public class ComponentViewBuilderDefinition<TComponent, TViewBuilder> : ComponentViewBuilderDefinition
        where TComponent : Sitecore.Commerce.Core.Component
        where TViewBuilder : IComponentViewBuilder
    {
        public ComponentViewBuilderDefinition() : base(typeof(TComponent), typeof(TViewBuilder))
        {
        }
    }

    public abstract class ComponentViewBuilderDefinition
    {
        public Type Component { get; }
        public Type ViewBuilder { get; }

        public ComponentViewBuilderDefinition(Type component, Type viewBuilder)
        {
            Condition.Requires<Type>(component, nameof(component)).IsNotNull<Type>();
            Condition.Requires<Type>(viewBuilder, nameof(viewBuilder)).IsNotNull<Type>();

            Component = component;
            ViewBuilder = viewBuilder;
        }
    }
}
