namespace dotnetcore_xp_sxa_demo.LinkProviders
{
    using System;

    using Sitecore.Abstractions;
    using Sitecore.Data.Items;
    using Sitecore.Links.UrlBuilders;

    public class HeadlessLinkProvider : Sitecore.Links.LinkProvider
    {
        public HeadlessLinkProvider(BaseFactory factory) : base(factory)
        {
        }

        public override string GetItemUrl(Item item, ItemUrlBuilderOptions options)
        {
            var url = base.GetItemUrl(item, options);
            if (!VirtualPathIsNeeded(options, url))
            {
                return url;
            }

            return $"/headlessdemo{url}";
        }

        private static bool VirtualPathIsNeeded(ItemUrlBuilderOptions options, string url)
        {
            return options?.Site?.Name == "hcc-demo-site-3-embedded" && Uri.IsWellFormedUriString(url, UriKind.Relative);
        }
    }
}