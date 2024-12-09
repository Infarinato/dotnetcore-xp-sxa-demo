using Microsoft.AspNetCore.Mvc;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Exceptions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Attributes;
using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;

namespace aspnet_core_demodotcomsite.Controllers;

public class HomeController : Controller
{
    private readonly SitecoreSettings? settings;
    private readonly ILogger<DefaultController> logger;

    public HomeController(ILogger<DefaultController> logger, IConfiguration configuration)
    {
        this.settings = configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
        ArgumentNullException.ThrowIfNull(this.settings);
        this.logger = logger;
    }

    [UseSitecoreRendering]
    public IActionResult Index(Layout model)
    {
        IActionResult result = Empty;
        var request = this.HttpContext.GetSitecoreRenderingContext();
        if ((request?.Response?.HasErrors ?? false) && !this.IsPageEditingRequest(request))
        {
            foreach (var error in request.Response.Errors)
            {
                switch (error)
                {
                    case ItemNotFoundSitecoreLayoutServiceClientException:
                        result = this.View("NotFound");
                        break;
                    default:
                        this.logger.LogError(error, "{Message}", error.Message);
                        throw error;
                }
            }
        }
        else
        {
            result = this.View(model);
        }

        return result;
    }

    public IActionResult Error()
    {
        return this.View();
    }

    private bool IsPageEditingRequest(ISitecoreRenderingContext request)
    {
        return request.Controller?.HttpContext.Request.Path == (this.settings?.EditingPath ?? string.Empty);
    }
}