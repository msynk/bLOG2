using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework.Results;

namespace bLOG.Web.Framework.Handlers
{
    public class RssHandler : BaseHandler
    {
        protected override IHttpResult ProcessRequestInternal()
        {
            var allPosts = PostService.Instance.Query.OrderByDescending(p => p.PublishDate).ToList();
            return new RssHttpResult(WebConfig.Settings.BlogTitle + " rss feed", Syndicate(allPosts));
        }

        private List<SyndicationItem> Syndicate(IList<Post> posts)
        {
            var results = new List<SyndicationItem>();
            foreach (var post in posts)
            {
                var uri = new Uri(string.Format("{0}://{1}/post/{2}/{3}", Request.Url.Scheme, Request.Url.Authority, post.Id, post.Title.Replace(" ", "-")));
                var feedItem = new SyndicationItem(
                        title: post.Title,
                        content: post.Content,
                        itemAlternateLink: uri,
                        id: post.Id,
                        lastUpdatedTime: post.PublishDate
                        );
                feedItem.Authors.Add(new SyndicationPerson(post.Author, post.Author, uri.Host));
                results.Add(feedItem);
            }
            return results;
        }
    }
}