using System;
using System.Linq;
using System.Collections.Generic;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework.Handlers
{
    public class HomeHandler : BaseHandler
    {
        protected override string PageTitle { get; set; }

        public IViewResult Index()
        {
            PageTitle = string.Format("{0} - Home", WebConfig.Settings.BlogTitle);
            IQueryable<Post> query = PostService.Instance.Query;
            query = ExpandQuery(query);
            return GenerateView(query);
        }

        private IQueryable<Post> ExpandQuery(IQueryable<Post> query)
        {
            string s = QueryString("s"); // search
            string t = QueryString("t"); // tag
            if (!string.IsNullOrEmpty(s))
            {
                s = s.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(s) || p.Content.ToLower().Contains(s)/* || p.Keywords.ToLower().Contains(s)*/);
            }
            if (!string.IsNullOrEmpty(t))
            {
                query = query.Where(p => p.Keywords.Contains(t));
            }
            return query;
        }
        private IViewResult GenerateView(IQueryable<Post> query)
        {
            IViewResult view = View();
            int pageNumber = GetPageNumber();
            int pageSize = int.Parse(WebConfig.Settings.PageSize);
            int totalPages = GetTotalPages(query, pageSize);
            if (pageNumber > totalPages)
            {
                Response.Redirect("/");
            }
            SetPagingTokens(view, pageNumber, totalPages);
            IList<Post> posts = GetPosts(query, pageNumber, pageSize);
            view.UpdateToken("Summaries", RenderSummaries(posts));
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
        private int GetTotalPages(IQueryable<Post> query, int pageSize)
        {
            var totalPosts = query.Count();
            var totalPages = totalPosts / pageSize;
            if (totalPages * pageSize != totalPosts)
            {
                totalPages++;
            }
            return totalPages;
        }
        private IList<Post> GetPosts(IQueryable<Post> query, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var posts = query.OrderByDescending(p => p.PublishDate).Skip(skip).Take(pageSize).ToList();
            return posts;
        }
        private void SetPagingTokens(IViewResult view, int pageNumber, int totalPages)
        {
            view.UpdateToken("PageNumber", pageNumber);
            view.UpdateToken("TotalPages", totalPages);

            var prevPageNumber = pageNumber + 1;
            view.UpdateToken("PrevPage", prevPageNumber);
            view.UpdateToken("OlderPostsDisplay", prevPageNumber > totalPages ? "none" : "block");


            var nextPageNumber = pageNumber - 1;
            view.UpdateToken("NextPage", nextPageNumber);
            view.UpdateToken("NewerPostsDisplay", nextPageNumber < 1 ? "none" : "block");

            string queryString = "";
            string s = QueryString("s");
            string t = QueryString("t");

            if (!string.IsNullOrEmpty(s))
            {
                queryString += "&s=" + s;
            }
            if (!string.IsNullOrEmpty(t))
            {
                queryString += "&t=" + t;
            }
            view.UpdateToken("QueryString", queryString);
        }
        private string RenderSummaries(IEnumerable<Post> posts)
        {
            var postSummaryView = View("PostSummary");
            postSummaryView.UseLayout = false;
            var summaries = "";
            foreach (var post in posts)
            {
                postSummaryView.ResetTokens();
                postSummaryView.UpdateToken("Id", post.Id);
                postSummaryView.UpdateToken("Slug", post.Title.Replace(" ", "-"));
                postSummaryView.UpdateToken("Author", RenderAuthor(post.Author));
                postSummaryView.UpdateToken("Title", post.Title);
                var content = post.Content;
                var index = content.IndexOf("<summary>", StringComparison.Ordinal);
                var length = content.Length;
                index = index > 0 ? index : (length > 500 ? 500 : length);
                postSummaryView.UpdateToken("Content", content.Substring(0, index) + "...");
                postSummaryView.UpdateToken("PublishDate", post.PublishDate.ToString("D"));
                postSummaryView.UpdateToken("ViewsCount", post.ViewsCount);
                postSummaryView.UpdateToken("Keywords", RenderKeywords(post.Keywords));
                summaries += postSummaryView.Render();
            }
            return summaries;
        }

        private string RenderKeywords(string keywords)
        {
            if (string.IsNullOrEmpty(keywords)) return "";

            return string.Join(", ", keywords.Split(',').Select(k => string.Format(Strings.AnchorFormat, "/?t=" + k.Trim(), k.Trim())));
        }
        private string RenderAuthor(string author)
        {
            if (string.IsNullOrEmpty(author)) return author;

            string url = WebConfig.AppSettings[string.Format(Strings.UserUrlFormat, author)];
            if (string.IsNullOrEmpty(url)) return author;

            return string.Format(Strings.AnchorFormat, url, author);
        }
    }
}