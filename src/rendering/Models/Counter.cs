using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public class Counter : BaseModel
{
    [SitecoreComponentField]
    public TextField? Heading { get; set; }

    [SitecoreComponentField(Name = "Current Count Label")]
    public TextField? CurrentCountLabel { get; set; }

    [SitecoreComponentField(Name = "Submit Button Label")]
    public TextField? SubmitButtonLabel { get; set; }
}