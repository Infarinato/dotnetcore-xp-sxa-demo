using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using Sitecore.AspNetCore.SDK.RenderingEngine.Binding.Attributes;
using System.ServiceModel.Syndication;

namespace aspnet_core_demodotcomsite.Models;

using aspnet_core_demodotcomsite.Models.Feeds;

public class RssFeed : BaseModel
{
    [SitecoreComponentField(Name = "PromoText")]
    public TextField? FeedTitle { get; set; }

    public IEnumerable<SyndicationItem> FeedItems => BbcNewsFeed.GetItems();
}