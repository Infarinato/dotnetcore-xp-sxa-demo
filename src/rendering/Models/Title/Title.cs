using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Properties;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models.Title;

public class Title : BaseModel
{
    [SitecoreComponentField(Name = "data")]            
    public TitleData? Data { get; set; }

    public HyperLinkField Link =>
        new(
            new HyperLink
                {
                    Anchor = this.TitleLocation?.Url?.Path,
                    Title = this.TitleLocation?.Field?.JsonValue?.Value
                });

    public TextField Text => new(this.TitleLocation?.Field?.JsonValue?.Value ?? string.Empty);

    public TitleLocation? TitleLocation => this.Data?.DataSource ?? this.Data?.ContextItem;
}