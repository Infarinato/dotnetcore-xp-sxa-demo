namespace aspnet_core_demodotcomsite.Middleware;

using aspnet_core_demodotcomsite.Extensions;

using Sitecore.AspNetCore.SDK.RenderingEngine.Extensions;
using Sitecore.AspNetCore.SDK.RenderingEngine.Middleware;

public class EnhancedSitecoreRenderingEnginePipeline : RenderingEnginePipeline
{
    public override void Configure(IApplicationBuilder app)
    {
        app.UseSitecoreRenderingEngine();
        app.UseNotFoundRouting();
    }
}