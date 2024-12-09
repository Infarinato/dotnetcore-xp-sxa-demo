using System.Text.RegularExpressions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;

namespace aspnet_core_demodotcomsite.Models;

public partial class Container : BaseModel
{
    [GeneratedRegex("/mediaurl=\\\"([^\"]*)\\\"/", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MediaUrlPattern();

    [SitecoreComponentParameter(Name = "BackgroundImage")]
    public string? BackgroundImage { get; set; }

    public string? BackgroundStyle
    { 
        get
        {
            if (!string.IsNullOrEmpty(this.BackgroundImage) && MediaUrlPattern().IsMatch(this.BackgroundImage))
            {
                var mediaUrl = MediaUrlPattern().Match(this.BackgroundImage).Groups[1].Value;
                return $"backgroundImage: url('{mediaUrl}')";
            }

            return string.Empty;
        }
    }

    public bool OutputContainerWrapper 
    {
        get
        {
            return this.Styles?.Split(' ').Any(x => x == "container") == true;
        }
    }
}