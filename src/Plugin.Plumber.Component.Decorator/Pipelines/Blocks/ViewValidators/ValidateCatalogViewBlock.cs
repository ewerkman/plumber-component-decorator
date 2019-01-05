﻿using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using System.Collections.Generic;

namespace Plugin.Plumber.Component.Decorator.Pipelines.Blocks.ViewValidators
{
    public class ValidateCatalogViewBlock : ValidateEntityViewBaseBlock<Catalog>
    {
        protected override List<string> GetApplicableViewNames(CommercePipelineExecutionContext context)
        {
            var viewsPolicy = context.GetPolicy<KnownCatalogViewsPolicy>();
            return new List<string>() { viewsPolicy?.Master };
        }
    }
}
