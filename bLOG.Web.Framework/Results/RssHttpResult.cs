using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;

namespace bLOG.Web.Framework.Results
{
  public class RssHttpResult : IHttpResult
  {
    private readonly string _feedTitle;
    readonly List<SyndicationItem> _rssItems;
    private readonly string _language;

    public RssHttpResult(string feedTitle, List<SyndicationItem> rssItems, string language = "en-US")
    {
      _feedTitle = feedTitle;
      _rssItems = rssItems;
      _language = language;
    }

    public void ExecuteResult(HttpContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");

      writeToResponse(context.Response);
      context.Response.Flush();
      context.Response.End();
    }

    private void writeToResponse(HttpResponse response)
    {
      var feed = new SyndicationFeed
      {
        Title = new TextSyndicationContent(_feedTitle),
        Language = _language,
        Items = _rssItems
      };
      response.ContentEncoding = Encoding.UTF8;
      response.ContentType = "application/rss+xml";
      using (var rssWriter = XmlWriter.Create(response.Output, new XmlWriterSettings { Indent = true }))
      {
        var formatter = new Rss20FeedFormatter(feed);
        formatter.WriteTo(rssWriter);
        rssWriter.Close();
      }
    }
  }
}
