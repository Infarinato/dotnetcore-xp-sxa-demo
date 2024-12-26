namespace aspnet_core_demodotcomsite.Extensions;

using aspnet_core_demodotcomsite.Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseNotFoundRouting(this IApplicationBuilder app)
    {
        app.UseMiddleware<NotFoundRoutingMiddleware>();
        return app;
    }
}