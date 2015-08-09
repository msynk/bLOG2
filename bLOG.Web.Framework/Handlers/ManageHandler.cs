using System.Collections.Generic;
using System.Linq;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework.Results.ViewResults;
using bLOG.Web.Framework.Services;

namespace bLOG.Web.Framework.Handlers
{
  public class ManageHandler : BaseHandler
  {
    public IViewResult Index()
    {
      Title = "Manage!";
      return View();
    }

    public IViewResult ShowAllPosts()
    {
      var username = Param("username");
      var password = Param("password");
      if (!SecurityService.Authenticate(username, password))
      {
        return null;
      }


      IEnumerable<Post> posts;
      if (username == "admin")
      {
        posts = PostService.Instance.Get();
      }
      else
      {
        posts = PostService.Instance.GetByAuthor(username);
      }
      var view = View();
      var rows = posts.Select(GetRowView).Aggregate("", (current, rowView) => current + rowView.Render());
      view.UpdateToken("PostRows", rows);
      return view;
    }

    private IViewResult GetRowView(Post post)
    {
      var view = View("PostRow");
      view.UseLayout = false;
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Title", post.Title);
      view.UpdateToken("SiteUrl", Request.Url.Authority);
      return view;
    }

    public IViewResult ConfirmDelete()
    {
      var post = PostService.Instance.Get(Id);
      if (post == null) return null;
      var view = View();
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Title", post.Title);
      return view;
    }

    public IViewResult Delete()
    {
      var username = Param("username");
      var password = Param("password");
      if (!SecurityService.Authenticate(username, password))
      {
        return null;
      }

      var postService = PostService.Instance;
      postService.Delete(postService.Get(Id));

      return new RedirectViewResult("/Manage");
    }
  }
}