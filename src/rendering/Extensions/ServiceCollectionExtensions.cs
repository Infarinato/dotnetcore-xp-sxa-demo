using aspnet_core_demodotcomsite.Models.LinkList;
using aspnet_core_demodotcomsite.Models.Navigation;
using aspnet_core_demodotcomsite.Models.Title;
using Sitecore.AspNetCore.SDK.RenderingEngine.Configuration;

namespace aspnet_core_demodotcomsite.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static RenderingEngineOptions AddStarterKitViews(this RenderingEngineOptions renderingEngineOptions)
        {
            renderingEngineOptions.AddModelBoundView<Title>("Title")
                                  .AddModelBoundView<Container>("Container")
                                  .AddModelBoundView<ColumnSplitter>("ColumnSplitter")
                                  .AddModelBoundView<RowSplitter>("RowSplitter")
                                  .AddModelBoundView<PageContent>("PageContent")
                                  .AddModelBoundView<RichText>("RichText")
                                  .AddModelBoundView<Promo>("Promo")
                                  .AddModelBoundView<LinkList>("LinkList")
                                  .AddModelBoundView<Image>("Image")
                                  .AddModelBoundView<PartialDesignDynamicPlaceholder>("PartialDesignDynamicPlaceholder")
                                  .AddModelBoundView<Navigation>("Navigation");

            return renderingEngineOptions;
        }
    }
}