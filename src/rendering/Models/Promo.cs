using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class Promo : BaseModel
{
    public const string VariantWithText = "WithText";

    [SitecoreComponentField]
    public ImageField? PromoIcon { get; set; }

    [SitecoreComponentField]
    public TextField? PromoText { get; set; }

    [SitecoreComponentField]
    public HyperLinkField? PromoLink { get; set; }
}