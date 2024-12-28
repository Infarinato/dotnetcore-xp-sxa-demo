namespace aspnet_core_demodotcomsite.Models.Feeds;

using System.ServiceModel.Syndication;
using System.Xml;

public static class BbcNewsFeed
{
    private const string FeedUrl = "https://feeds.bbci.co.uk/news/rss.xml?edition=uk";

    public static IEnumerable<SyndicationItem> GetItems()
    {
        var feed = GetFeed();
        return feed.Items ?? new List<SyndicationItem>();
    }

    private static SyndicationFeed GetFeed()
    {
        var reader = XmlReader.Create(FeedUrl);
        var feed = SyndicationFeed.Load(reader);
        reader.Close();
        return feed;
    }

    public static ThumbnailImage GetThumbnailImage(SyndicationItem item)
    {
        foreach (var extension in item.ElementExtensions)
        {
            var element = extension.GetObject<XmlElement>();
            if (element.Name != "media:thumbnail" || !element.HasAttributes)
            {
                continue;
            }

            var url = element.GetAttribute("url");
            var height = element.GetAttribute("height");
            var width = element.GetAttribute("width");
            return new ThumbnailImage { Url = url, Height = height, Width = width};
        }

        return new ThumbnailImage();
    }
}