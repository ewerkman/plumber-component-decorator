using Microsoft.Extensions.DependencyInjection;
using Plugin.Plumber.Component.Decorator.Pipelines;
using Plugin.Plumber.Component.Decorator.Pipelines.Blocks;
using Plugin.Plumber.Component.Decorator.Pipelines.Blocks.ViewValidators;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Plugin.Plumber.Component
{
    public static class ServicesConfigurationExtensions
    {
        public static ISitecoreServicesConfiguration ConfigureGetEntityViewPipeline(this ISitecoreServicesConfiguration services)
        {
            services.Pipelines(
                config =>
                    config
                        .ConfigurePipeline<IGetEntityViewPipeline>(c =>
                        {
                            c
                            .Add<InitializeLocalizationPoliciesBlock>().After<PopulateEntityVersionBlock>()
                            .Add<GetComponentViewBlock>().After<GetSellableItemDetailsViewBlock>()
                            .Add<GetCatalogComponentConnectViewBlock>().After<GetComponentViewBlock>()
                            .Add<GetCategoryComponentConnectViewBlock>().After<GetCatalogComponentConnectViewBlock>()
                            .Add<GetSellableItemComponentConnectViewBlock>().After<GetCategoryComponentConnectViewBlock>();
                        })
            );
            return services;
        }

        public static ISitecoreServicesConfiguration ConfigurePopulateEntityViewActionsPipeline(this ISitecoreServicesConfiguration services)
        {
            services.Pipelines(
                config =>
                    config
                        .ConfigurePipeline<IPopulateEntityViewActionsPipeline>(c =>
                        {
                            c.Add<InitializeLocalizationPoliciesBlock>().After<InitializeEntityViewActionsBlock>()
                             .Add<PopulateComponentActionsBlock>().After<InitializeEntityViewActionsBlock>();
                        })
            );
            return services;
        }

        public static ISitecoreServicesConfiguration ConfigureDoActionPipeline(this ISitecoreServicesConfiguration services)
        {
            services.Pipelines(
                config =>
                    config
                        .ConfigurePipeline<IDoActionPipeline>(c =>
                        {
                            c.Add<InitializeLocalizationPoliciesBlock>().After<DoActionBlock>()
                            .Add<DoActionEditComponentBlock>().After<ValidateEntityVersionBlock>()
                            .Add<DoActionAddValidationConstraintBlock>().Before<DoActionEditComponentBlock>();
                        })
            );
            return services;
        }

        public static ISitecoreServicesConfiguration AddGetApplicableViewConditionsPipeline(this ISitecoreServicesConfiguration services)
        {
            services.Pipelines(
                config =>
                    config.
                        AddPipeline<IGetApplicableViewConditionsPipeline, GetApplicableViewConditionsPipeline>(c =>
                        {
                            c.Add<ValidateSellableItemViewBlock>()
                            .Add<ValidatePromotionViewBlock>()
                            .Add<ValidateOrderViewBlock>()
                            .Add<ValidateCustomerViewBlock>()
                            .Add<ValidateInventorySetBlock>()
                            .Add<ValidateCatalogViewBlock>()
                            .Add<ValidatePromotionBookViewBlock>()
                            .Add<ValidatePriceBookViewBlock>()
                            .Add<ValidatePriceCardViewBlock>()
                            .Add<ValidateCategoryViewBlock>();
                        })
            );
            return services;
        }
    }
}
