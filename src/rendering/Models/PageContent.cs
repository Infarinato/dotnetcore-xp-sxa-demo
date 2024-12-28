using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class PageContent : BaseModel
{
    [SitecoreRouteField(Name = "Content")]
    public RichTextField? RouteContent { get; set; }

    [SitecoreComponentField(Name = "Content")]
    public RichTextField? ComponentContent { get; set; }

    public RichTextField? Content => this.ComponentContent ?? this.RouteContent;
}