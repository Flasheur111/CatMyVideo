using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace RssFeeder
{
    public class CatMyVideoFeeder : IFeed
    {
        private static readonly int MAXVIDEO = 10;
        private static readonly int PORT = 30655;

        public SyndicationFeedFormatter CreateFeed()
        {
            // Create a new Syndication Feed.
            SyndicationFeed feed = new SyndicationFeed();

            feed.Id = "#CatMyVideo URL";


            List<SyndicationItem> items = new List<SyndicationItem>();

            feed.Title = new TextSyndicationContent("Hot trends on CatMyVideo");
            feed.Description = new TextSyndicationContent(String.Format("Todays' top {0} hottest videos on CatMyVideo", MAXVIDEO));
            feed.Copyright = new TextSyndicationContent("Copy/Paste rights CatMyVideo");
            feed.Generator = "CatMyVideo RSS Feeder 1.0";
            feed.Authors.Add(new SyndicationPerson("contact@catmayvideo.com"));

            // Todo : set image URI
            /**
             * string imageUrl = "";
             * feed.ImageUrl = new Uri(imageUrl); 
             */

            feed.LastUpdatedTime = new DateTimeOffset(DateTime.Now);

            var trendingVideos = Engine.BusinessManagement.Video.ListVideos(Engine.Dbo.Video.Order.ViewCountToday, false, MAXVIDEO);
            for (int i = 0; i < trendingVideos.Count; i++)
            {
                SyndicationItem item = new SyndicationItem();

                string itemUrl = "http://" + WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.DnsSafeHost + ':' + PORT + "/api/videoApi/" + trendingVideos[i].Id;
                item.Id = itemUrl;

                var itemLink = new SyndicationLink(new Uri(itemUrl));
                itemLink.MediaType = "text/html";
                itemLink.Title = "Watch me !";
                item.Links.Add(itemLink);

                string htmlContent = String.Format("<!DOCTYPE html><html><head></head><body><h1>{0}</h1><p>{1}</p><a href=\"{2}\">Check this out !</a></body></html>",
                                                    trendingVideos[i].Title,
                                                    trendingVideos[i].Description,
                                                    itemUrl);
                TextSyndicationContent content = new TextSyndicationContent(htmlContent, TextSyndicationContentKind.Html);
               
                // Fill some properties for the item
                item.Title = new TextSyndicationContent("#" + (i + 1));
                item.LastUpdatedTime = DateTime.Now;
                item.PublishDate = trendingVideos[i].UploadDate;

                item.Content = content;
                items.Add(item);
            }
            feed.Items = items;

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
