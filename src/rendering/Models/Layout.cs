using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class Layout
{
    [SitecoreRouteField]
    public string? Name { get; set; }

    [SitecoreRouteField]
    public string? DisplayName { get; set; }

    [SitecoreRouteField]
    public string? ItemId { get; set; }

    [SitecoreRouteField]
    public string? ItemLanguage { get; set; }

    [SitecoreRouteField]
    public string? TemplateId { get; set; }

    [SitecoreRouteField]
    public string? TemplateName { get; set; }

    public string? VirtualFolder { get; set; }
}