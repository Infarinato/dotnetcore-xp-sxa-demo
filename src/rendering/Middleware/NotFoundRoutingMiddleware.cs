namespace aspnet_core_demodotcomsite.Middleware;

using System.Net;

using Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

using Sitecore.AspNetCore.SDK.LayoutService.Client.Exceptions;
using Sitecore.AspNetCore.SDK.LayoutService.Client.Interfaces;
using Sitecore.AspNetCore.SDK.RenderingEngine;
using Sitecore.AspNetCore.SDK.RenderingEngine.Configuration;
using Sitecore.AspNetCore.SDK.RenderingEngine.Extensions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Interfaces;
using Sitecore.AspNetCore.SDK.RenderingEngine.Middleware;

public class NotFoundRoutingMiddleware
{
    private readonly RequestDelegate next;

    private readonly ILogger<NotFoundRoutingMiddleware> logger;

    private readonly SitecoreSettings? settings;

    private readonly RenderingEngineMiddleware renderingEngine;

    public NotFoundRoutingMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        ILogger<NotFoundRoutingMiddleware> logger,
        ISitecoreLayoutRequestMapper requestMapper,
        ISitecoreLayoutClient layoutService,
        IOptions<RenderingEngineOptions> options)
    {
        this.next = next;
        this.settings = configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
        this.logger = logger;
        this.renderingEngine = new RenderingEngineMiddleware(this.next, requestMapper, layoutService, options);
    }

    public async Task InvokeAsync(HttpContext context, IViewComponentHelper viewComponentHelper, IHtmlHelper htmlHelper)
    {
        var sitecoreContext = context.GetSitecoreRenderingContext();
        if (sitecoreContext != null && (sitecoreContext.Response?.HasErrors ?? false))
        {
            var notFound = sitecoreContext.Response.Errors.OfType<ItemNotFoundSitecoreLayoutServiceClientException>().FirstOrDefault();
            if (notFound != null)
            {
                // [IVA] Keep track of not found pages in info logs
                this.logger.LogInformation(notFound, notFound.Message);

                // [IVA] Change the request faking it towards the 404 page
                context.Request.Path = this.settings?.NotFoundPage;

                // [IVA] Force the response to use 404 status code
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                // [IVA] Bust the RenderingEngine execution cache
                context.Items.Remove(nameof(RenderingEngineMiddleware));
                context.Features.Set<ISitecoreRenderingContext>(null);
                if (context.Request.RouteValues.ContainsKey(RenderingEngineConstants.RouteValues.SitecoreRoute))
                {
                    context.Request.RouteValues.Remove(RenderingEngineConstants.RouteValues.SitecoreRoute);
                }

                // [IVA] Finally we re-run the RenderingEngine
                await this.renderingEngine.Invoke(context, viewComponentHelper, htmlHelper);
            }
            else
            {
                await this.next(context);
            }
        }
        else
        {
            await this.next(context);
        }
    }
}