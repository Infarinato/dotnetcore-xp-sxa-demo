using Microsoft.AspNetCore.Mvc;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Exceptions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Attributes;
using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;
using aspnet_core_demodotcomsite.Models;

namespace aspnet_core_demodotcomsite.Controllers;

public class HomeController : Controller
{
    private readonly SitecoreSettings? _settings;
    private readonly ILogger<DefaultController> _logger;


    public HomeController(ILogger<DefaultController> logger, IConfiguration configuration)
    {
        _settings = configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
        ArgumentNullException.ThrowIfNull(_settings);
        _logger = logger;
    }

    [UseSitecoreRendering]
    public IActionResult Index(Layout model)
    {
        IActionResult result = Empty;
        ISitecoreRenderingContext? request = HttpContext.GetSitecoreRenderingContext();
        if ((request?.Response?.HasErrors ?? false) && !IsPageEditingRequest(request))
        {
            foreach (SitecoreLayoutServiceClientException error in request.Response.Errors)
            {
                switch (error)
                {
                    case ItemNotFoundSitecoreLayoutServiceClientException:
                        result = View("NotFound");
                        break;
                    default:
                        _logger.LogError(error, "{Message}", error.Message);
                        throw error;
                }
            }
        }
        else
        {
            result = View(model);
        }

        return result;
    }

    public IActionResult Error()
    {
        return View();
    }

    private bool IsPageEditingRequest(ISitecoreRenderingContext request)
    {
        return request.Controller?.HttpContext.Request.Path == (_settings?.EditingPath ?? string.Empty);
    }
}