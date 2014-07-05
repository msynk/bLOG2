using System.Collections.Generic;
using System.Linq;
using bLOG.Core.Domain;
using bLOG.Data.Services;
using bLOG.Web.Framework;
using bLOG.Web.Framework.Views;

namespace bLOG.Web.Framework.Handlers
{
  public class ManageHandler : BaseHandler
  {
    public IView Index()
    {
      Title = "Manage!";
      return View();
    }

    public IView ShowAllPosts()
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

    private IView GetRowView(Post post)
    {
      var view = View("PostRow");
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Title", post.Title);
      view.UpdateToken("SiteUrl", Request.Url.Authority);
      return view;
    }

    public IView ConfirmDelete()
    {
      var post = PostService.Instance.Get(Id);
      if (post == null) return null;
      var view = View();
      view.UpdateToken("Id", post.Id);
      view.UpdateToken("Title", post.Title);
      return view;
    }

    public IView Delete()
    {
      var username = Param("username");
      var password = Param("password");
      if (!SecurityService.Authenticate(username, password))
      {
        return null;
      }

      var postService = PostService.Instance;
      postService.Delete(postService.Get(Id));

      return new RedirectView("/Manage");
    }
  }
}