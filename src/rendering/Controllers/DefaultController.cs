namespace aspnet_core_demodotcomsite.Controllers;

using aspnet_core_demodotcomsite.Middleware;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;

using Sustainsys.Saml2.AspNetCore2;

public class DefaultController : HccController
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
        else if (this.IsAuthenticationRequired(request))
        {
            AuthenticationProperties properties = new()
            {
                RedirectUri = HttpContext.Request.GetEncodedUrl()
            };

            result = Challenge(properties, Saml2Defaults.Scheme);
        }
        else
        {
            model = GetLayout(model, request);
            result = this.View(model);
        }

        return result;
    }

    private bool IsAuthenticationRequired(ISitecoreRenderingContext? request)
    {
        return !this.UserIsAuthenticated() && IsSecurePage(request) && !PageIsInEditingMode(request);
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

    private static bool PageIsInEditingMode(ISitecoreRenderingContext? request)
    {
        return request?.Response?.Content?.Sitecore?.Context?.IsEditing ?? false;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View();
    }
}