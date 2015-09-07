using System.Linq;
using bLOG.Data.Services;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework.Handlers
{
    public class PostHandler : BaseHandler
    {
        public IViewResult Index()
        {
            var postService = PostService.Instance;
            var post = postService.Get(Id);
            if (post == null) return null;

            PageTitle = string.Format("{0} - {1}", WebConfig.Settings.BlogTitle, post.Title);
            post.ViewsCount += 1;
            postService.Edit(post);

            var view = View();
            view.UpdateToken("Id", post.Id);
            view.UpdateToken("Slug", post.Title.Replace(" ", "-"));
            view.UpdateToken("Author", RenderAuthor(post.Author));
            view.UpdateToken("Title", post.Title);
            view.UpdateToken("Content", post.Content);
            view.UpdateToken("PublishDate", post.PublishDate.ToString("D"));
            view.UpdateToken("ViewsCount", post.ViewsCount.ToString());
            view.UpdateToken("Keywords", RenderKeywords(post.Keywords));

            return view;
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