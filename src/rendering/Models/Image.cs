using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class Image : BaseModel
{
    public const string VariantBanner = "Banner";

    [SitecoreComponentField]
    public HyperLinkField? TargetUrl { get; set; }

    [SitecoreComponentField(Name = "Image")]
    public ImageField? ImageField { get; set; }

    [SitecoreComponentField]
    public TextField? ImageCaption { get; set; }
}