namespace aspnet_core_demodotcomsite.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HccController : Controller
{
    protected bool UserIsAuthenticated()
    {
        return this.HttpContext.User.Identity?.IsAuthenticated ?? false;
    }
}