using Sitecore.AspNetCore.SDK.LayoutService.Client.Serialization.Converter;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models.Navigation;

public class Navigation : BaseModel
{
    public Navigation() { }

    public Navigation(List<NavigationItem>? navigationItems, int? menuLevel)
    {
        this.NavigationItems = navigationItems;
        this.MenuLevel = menuLevel;
    }

    [SitecoreComponentField(Name = FieldParser.CustomContentFieldKey)]
    public List<NavigationItem>? NavigationItems { get; set; }

    public int? MenuLevel { get; set; }
}