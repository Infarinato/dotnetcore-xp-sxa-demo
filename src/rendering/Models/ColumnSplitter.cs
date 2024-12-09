using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class ColumnSplitter : RowSplitter
{
    [SitecoreComponentParameter]
    public string? ColumnWidth1 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth2 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth3 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth4 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth5 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth6 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth7 { get; set; }

    [SitecoreComponentParameter]
    public string? ColumnWidth8 { get; set; }

    public string[] ColumnWidths =>
        [
            this.ColumnWidth1 ?? string.Empty, this.ColumnWidth2 ?? string.Empty, this.ColumnWidth3 ?? string.Empty, this.ColumnWidth4 ?? string.Empty, this.ColumnWidth5 ?? string.Empty, this.ColumnWidth6 ?? string.Empty, this.ColumnWidth7 ?? string.Empty, this.ColumnWidth8 ?? string.Empty,
        ];
}