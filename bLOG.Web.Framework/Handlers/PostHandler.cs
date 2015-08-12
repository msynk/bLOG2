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

      PageTitle = string.Format("{0} - {1}", WebConfig.BlogTitle, post.Title);
      post.ViewsCount += 1;
      postService.Edit(post);

      var view = View();
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Slug", post.Title.Replace(" ", "-"));
      view.UpdateToken("Author", post.Author);
      view.UpdateToken("Title", post.Title);
      view.UpdateToken("Content", post.Content);
      view.UpdateToken("PublishDate", post.PublishDate.ToString("D"));
      view.UpdateToken("ViewsCount", post.ViewsCount.ToString());
      view.UpdateToken("Keywords", post.Keywords ?? "");

      return view;
    }
  }
}