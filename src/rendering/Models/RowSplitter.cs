using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class RowSplitter : BaseModel
{
    [SitecoreComponentParameter]
    public string? EnabledPlaceholders { get; set; }

    [SitecoreComponentParameter]
    public string? Styles1 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles2 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles3 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles4 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles5 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles6 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles7 { get; set; }

    [SitecoreComponentParameter]
    public string? Styles8 { get; set; }

    public string[] ColumnStyles =>
        [
            this.Styles1 ?? string.Empty, this.Styles2 ?? string.Empty, this.Styles3 ?? string.Empty, this.Styles4 ?? string.Empty, this.Styles5 ?? string.Empty, this.Styles6 ?? string.Empty, this.Styles7 ?? string.Empty, this.Styles8 ?? string.Empty,
        ];

    public int[] EnabledPlaceholderIds => this.EnabledPlaceholders?.Split(',').Select(int.Parse).ToArray() ?? [];
}