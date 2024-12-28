using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class PartialDesignDynamicPlaceholder : BaseModel
{
    [SitecoreComponentParameter(Name ="sig")]
    public string? Sig { get; set; }
}