using Microsoft.AspNetCore.Mvc;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Exceptions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Attributes;
using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;

namespace aspnet_core_demodotcomsite.Controllers;

public class DefaultController : Controller
{
    private readonly SitecoreSettings? settings;
    private readonly ILogger<DefaultController> logger;

    public DefaultController(ILogger<DefaultController> logger, IConfiguration configuration)
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
            model = GetLayout(model, request);
            result = this.View(model);
        }

        return result;
    }

    private static Layout GetLayout(Layout model, ISitecoreRenderingContext? request)
    {
        if (!string.IsNullOrWhiteSpace(model.ItemId))
        {
            return model;
        }

        var route = request?.Response?.Content?.Sitecore?.Route;
        if (route == null)
        {
            return model;
        }

        return new Layout
        {
            DisplayName = string.IsNullOrWhiteSpace(route.DisplayName) ? route.Name : route.DisplayName,
            ItemId = route.ItemId,
            ItemLanguage = route.ItemLanguage,
            Name = route.Name,
            TemplateId = route.TemplateId,
            TemplateName = route.TemplateName
        };
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