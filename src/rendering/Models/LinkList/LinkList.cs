using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models.LinkList;

public class LinkList : BaseModel
{
    [SitecoreComponentField(Name = "data")]
    public DataField? Data { get; set; }

    public TextField? Title => this.Data?.Datasource?.Field?.Title;

    public List<LinkListItem> Children => this.Data?.Datasource?.Children?.Results ?? [];
}