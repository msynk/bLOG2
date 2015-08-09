using System;
using System.Collections.Generic;
using System.Linq;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework.Handlers
{
  public class HomeHandler : BaseHandler
  {
    protected override string Title { get; set; }

    public IViewResult Index()
    {
      Title = "bLOG2";
      var view = View();
      IEnumerable<Post> posts;

      var q = QueryString("q");
      if (!string.IsNullOrEmpty(q))
      {
        posts = SearchPosts(q);

        view.UpdateToken("OlderPostsDisplay", "none");
        view.UpdateToken("NewerPostsDisplay", "none");
      }
      else
      {
        var pageNumber = GetPageNumber();
        var pageSize = int.Parse(WebConfig.AppSettings["Index.PageSize"]);
        var totalPages = GetTotalPages(pageSize);
        if (pageNumber > totalPages)
        {
          Response.Redirect("/");
        }
        view.UpdateToken("FirstPageTitleDisplay", pageNumber == 1 ? "block" : "none");
        RenderPaging(view, pageNumber, totalPages);
        posts = GetPosts(pageNumber, pageSize);
      }

      RenderPosts(view, posts);
      return view;
    }


    private int GetPageNumber()
    {
      int pageNumber;
      var number = QueryString("p");
      if (!int.TryParse(number, out pageNumber))
      {
        pageNumber = 1;
      }
      return pageNumber;
    }
    private static IList<Post> GetPosts(int pageNumber, int pageSize)
    {
      var skip = (pageNumber - 1) * pageSize;
      var posts = PostService.Instance.Query.OrderByDescending(p => p.PublishDate).Skip(skip).Take(pageSize).ToList();
      return posts;
    }
    private static int GetTotalPages(int pageSize)
    {
      var totalPosts = PostService.Instance.Query.Count();
      var totalPages = totalPosts / pageSize;
      if (totalPages * pageSize != totalPosts)
      {
        totalPages++;
      }
      return totalPages;
    }
    private static IList<Post> SearchPosts(string q)
    {
      var posts = PostService.Instance.Query.Where(p => p.Title.Contains(q)).OrderByDescending(p => p.PublishDate).ToList();
      return posts;
    }

    private static void RenderPaging(IViewResult view, int pageNumber, int totalPages)
    {
      view.UpdateToken("PageNumber", pageNumber);
      view.UpdateToken("TotalPages", totalPages);

      var prevPageNumber = pageNumber + 1;
      view.UpdateToken("PrevPage", prevPageNumber);
      view.UpdateToken("OlderPostsDisplay", prevPageNumber > totalPages ? "none" : "inline");
      

      var nextPageNumber = pageNumber - 1;
      view.UpdateToken("NextPage", nextPageNumber);
      view.UpdateToken("NewerPostsDisplay", nextPageNumber < 1 ? "none" : "inline");
    }
    private void RenderPosts(IViewResult view, IEnumerable<Post> posts)
    {
      var postSummaryView = View("PostSummary");
      postSummaryView.UseLayout = false;
      var summaries = "";
      foreach (var post in posts)
      {
        postSummaryView.ResetTokens();
        postSummaryView.UpdateToken("Id", post.Id);
        postSummaryView.UpdateToken("Slug", post.Title.Replace(" ", "-"));
        postSummaryView.UpdateToken("Author", post.Author);
        postSummaryView.UpdateToken("Title", post.Title);
        var content = post.Content;
        var index = content.IndexOf("<summary>", StringComparison.Ordinal);
        var length = content.Length;
        index = index > 0 ? index : (length > 500 ? 500 : length);
        postSummaryView.UpdateToken("Content", content.Substring(0, index) + "...");
        postSummaryView.UpdateToken("PublishDate", post.PublishDate.ToString("D"));
        postSummaryView.UpdateToken("ViewsCount", post.ViewsCount);
        postSummaryView.UpdateToken("Keywords", post.Keywords ?? "");
        summaries += postSummaryView.Render();
      }
      view.UpdateToken("Summaries", summaries);
    }
  }
}