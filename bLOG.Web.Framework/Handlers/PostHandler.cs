using System.Globalization;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework.Views;

namespace bLOG.Web.Framework.Handlers
{
  public class PostHandler : BaseHandler
  {
    public IView Index()
    {
      var post = PostService.Get(Id);
      if (post == null) return null;

      Title = post.Title;
      post.ViewsCount += 1;
      PostService.Edit(post);

      var view = View();
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Slug", post.Title.Replace(" ", "-"));
      view.UpdateToken("Author", post.Author);
      view.UpdateToken("Title", post.Title);
      view.UpdateToken("Content", post.Content);
      view.UpdateToken("PublishDate", post.PublishDate.ToString("D"));
      view.UpdateToken("ViewsCount", post.ViewsCount.ToString());

      return view;
    }
  }
}