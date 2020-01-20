using Plugin.Plumber.Component.Decorator.Builders;
using Sitecore.Commerce.EntityViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component.Decorator.Commanders
{
    public class EntityViewCommander : ViewCommander 
    {
        public IServiceProvider ServiceProvider { get; }

        private readonly IEnumerable<EntityViewDefinition> entityViews;

        public EntityViewCommander(IServiceProvider serviceProvider, IEnumerable<EntityViewDefinition> entityViews) : base(serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.entityViews = entityViews;
        }

        public List<Type> GetAllEntityTypes()
        {
            return entityViews.Select(comp => comp.Defines).ToList<Type>();
        }
    }
}
