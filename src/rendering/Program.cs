using System.Globalization;
using System.Security.Cryptography.X509Certificates;

using aspnet_core_demodotcomsite.Extensions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Sitecore.AspNetCore.SDK.ExperienceEditor.Extensions;

using Sustainsys.Saml2;
using Sustainsys.Saml2.AspNetCore2;
using Sustainsys.Saml2.Metadata;

var builder = WebApplication.CreateBuilder(args);

var sitecoreSettings = builder.Configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
ArgumentNullException.ThrowIfNull(sitecoreSettings);

builder.Services.AddRouting()
                .AddLocalization()
                .AddMvc();

builder.Services.AddSitecoreLayoutService()
                .AddGraphQlHandler("default", sitecoreSettings.DefaultSiteName!, sitecoreSettings.ExperienceEdgeToken!, sitecoreSettings.LayoutServiceUri!)
                .AsDefaultHandler();

builder.Services.AddAuthentication(options =>
        {
            // Default scheme that maintains session is cookies.
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            // If there's a challenge to sign in, use the Saml2 scheme.
            options.DefaultChallengeScheme = Saml2Defaults.Scheme;
        })
    .AddCookie()
    .AddSaml2(opt =>
        {
            // Set up our EntityId, this is our application.
            opt.SPOptions.EntityId = new EntityId("https://headlessdemo.hants.gov.uk");

            // Single logout messages should be signed according to the SAML2 standard, so we need
            // to add a certificate for our app to sign logout messages with to enable logout functionality.
            //opt.SPOptions.ServiceCertificates.Add(new X509Certificate2("okta.cert"));

            // Add an identity provider.
            opt.IdentityProviders.Add(new IdentityProvider(
                                          // The identity provider's entity ID.
                                          new EntityId("http://www.okta.com/exkm1a39rvf16yMyx5d7"),
                                          opt.SPOptions)
            {
                // Load config parameters from metadata
                MetadataLocation = "https://dev-52855620.okta.com/app/exkm1a39rvf16yMyx5d7/sso/saml/metadata",
                LoadMetadata = true
            });
        });

builder.Services.AddSitecoreRenderingEngine(options =>
{
    options.AddStarterKitViews()
           .AddDefaultPartialView("_ComponentNotFound");
})
                .ForwardHeaders()
                .WithExperienceEditor(options => { options.JssEditingSecret = sitecoreSettings.EditingSecret ?? string.Empty; });

builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

if (sitecoreSettings.EnableEditingMode)
{
    app.UseSitecoreExperienceEditor();
}

app.UseRouting();
app.UseStaticFiles();

const string defaultLanguage = "en";
app.UseRequestLocalization(options =>
{
    // If you add languages in Sitecore which this site / Rendering Host should support, add them here.
    List<CultureInfo> supportedCultures = [new(defaultLanguage)];
    options.DefaultRequestCulture = new RequestCulture(defaultLanguage, defaultLanguage);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.UseSitecoreRequestLocalization();
});

app.MapControllerRoute(
        "error",
        "error",
        new { controller = "Default", action = "Error" }
    );

app.MapBlazorHub();
app.MapSitecoreLocalizedRoute("sitecore", "Index", "Default");
app.MapFallbackToController("Index", "Default");

app.Run();