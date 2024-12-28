namespace aspnet_core_demodotcomsite.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sustainsys.Saml2.AspNetCore2;

public class UserController : HccController
{
    public IActionResult SignIn(string? returnUrl)
    {
        var postSignInUrl = GetValidPostSignInUrl(returnUrl);
        if (this.UserIsAuthenticated())
        {
            return this.Redirect(postSignInUrl);
        }

        var properties = new AuthenticationProperties
        {
            RedirectUri = postSignInUrl
        };

        return this.Challenge(properties, Saml2Defaults.Scheme);

    }

    private static string GetValidPostSignInUrl(string? returnUrl)
    {
        const string DefaultPostSignInUrl = "/";
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return DefaultPostSignInUrl;
        }

        return Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : DefaultPostSignInUrl;
    }

    public IActionResult Claims()
    {
        return this.View();
    }

    public new IActionResult SignOut()
    {
        const string PostLogoutUrl = "/";

        return new SignOutResult(
            [
                Saml2Defaults.Scheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            ],
            new AuthenticationProperties { RedirectUri = PostLogoutUrl });
    }
}