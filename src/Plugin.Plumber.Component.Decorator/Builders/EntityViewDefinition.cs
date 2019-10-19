using Sitecore.Framework.Conditions;
using System;

namespace Plugin.Plumber.Component.Decorator.Builders
{
    public class EntityViewDefinition<TEntity> : EntityViewDefinition where TEntity : Sitecore.Commerce.Core.CommerceEntity
    {   
        public EntityViewDefinition() : base(typeof(TEntity))
        {
        }
    }

    public abstract class EntityViewDefinition
    {
        public Type Defines { get; }

        public EntityViewDefinition(Type defines)
        {
            Condition.Requires<Type>(defines, nameof(defines)).IsNotNull<Type>();
            Defines = defines;
        }
    }
}