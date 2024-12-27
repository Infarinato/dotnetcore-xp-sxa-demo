using Microsoft.AspNetCore.Mvc;

using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;

namespace aspnet_core_demodotcomsite.Controllers;

using aspnet_core_demodotcomsite.Middleware;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;

public class DefaultController : Controller
{
    private readonly ILogger<DefaultController> logger;

    public DefaultController(ILogger<DefaultController> logger, IConfiguration configuration)
    {
        var settings = configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
        ArgumentNullException.ThrowIfNull(settings);
        this.logger = logger;
    }

    [UseEnhancedSitecoreRendering]
    public IActionResult Index(Layout model)
    {
        IActionResult result = Empty;
        var request = this.HttpContext.GetSitecoreRenderingContext();
        if (request?.Response?.HasErrors ?? false)
        {
            foreach (var error in request.Response.Errors)
            {
                switch (error)
                {
                    default:
                        this.logger.LogError(error, "{Message}", error.Message);
                        throw error;
                }
            }
        }
        else if (!(HttpContext.User.Identity?.IsAuthenticated ?? false) && IsSecurePage(request) && !(request?.Response?.Content?.Sitecore?.Context?.IsEditing ?? false))
        {
            AuthenticationProperties properties = new()
            {
                RedirectUri = HttpContext.Request.GetEncodedUrl()
            };

            result = Challenge(properties);
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

    private static bool IsSecurePage(ISitecoreRenderingContext? request)
    {
        var result = false;
        if (request?.Response?.Content?.Sitecore?.Route?.Fields.TryGetValue("RequiresAuthentication", out var requiresAuthFieldReader) ?? false)
        {
            result = requiresAuthFieldReader.Read<CheckboxField>()?.Value ?? false;
        }

        return result;
    }
}