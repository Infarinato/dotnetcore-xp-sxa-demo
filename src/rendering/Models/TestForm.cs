using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class TestForm : BaseModel
{
    [SitecoreComponentField]
    public TextField? DateLabel { get; set; }

    [SitecoreComponentField]
    public TextField? EntryLabel { get; set; }

    [SitecoreComponentField]
    public TextField? SubmittedDateHeading { get; set; }

    [SitecoreComponentField]
    public TextField? SubmittedEntryHeading { get; set; }
}