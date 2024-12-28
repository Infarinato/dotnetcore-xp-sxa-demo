namespace aspnet_core_demodotcomsite.Middleware;

using Microsoft.AspNetCore.Mvc;

public class UseEnhancedSitecoreRendering(Type configurationType) : MiddlewareFilterAttribute(configurationType)
{
    public UseEnhancedSitecoreRendering() : this(typeof(EnhancedSitecoreRenderingEnginePipeline))
    {
    }
}