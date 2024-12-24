using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public abstract class BaseModel
{
    [SitecoreContextProperty]
    public bool IsEditing { get; set; }

    [SitecoreComponentProperty(Name = nameof(Component.Id))]
    public string? Id { get; set; }

    [SitecoreComponentProperty(Name = nameof(Component.Name))]
    public string? ComponentName { get; set; }

    [SitecoreComponentParameter]
    public string? GridParameters { get; set; }

    [SitecoreComponentParameter]
    public string? FieldNames { get; set; }

    [SitecoreComponentParameter]
    public string? Styles { get; set; }

    [SitecoreComponentParameter]
    public int DynamicPlaceholderId { get; set; } = 1;
}