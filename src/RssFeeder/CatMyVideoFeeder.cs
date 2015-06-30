using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace RssFeeder
{
    public class CatMyVideoFeeder : IFeed
    {
        public SyndicationFeedFormatter CreateFeed()
        {
            // Create a new Syndication Feed.
            SyndicationFeed feed = new SyndicationFeed("Cat My Video", "Hot trends on Cat My Video", null);
            List<SyndicationItem> items = new List<SyndicationItem>();

            var trendingVideos = Engine.BusinessManagement.Video.ListVideos(Engine.Dbo.Video.Order.ViewCount, false, 10);

            for (int i = 0; i < trendingVideos.Count; i++)
            {
                SyndicationItem item = new SyndicationItem(trendingVideos[i].Title, trendingVideos[i].Description, null);
                items.Add(item);
            }
            
            feed.Items = items;

            // Return ATOM or RSS based on query string
            // rss -> http://localhost:8733/Design_Time_Addresses/RssFeeder/Feed1/
            // atom -> http://localhost:8733/Design_Time_Addresses/RssFeeder/Feed1/?format=atom
            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }

            return formatter;
        }
    }
}
