using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using bLOG.Data.Services;
using bLOG.Web.Framework.MetaWeblog.Entities;
using bLOG.Web.Framework.Services;
using CookComputing.XmlRpc;

namespace bLOG.Web.Framework.MetaWeblog
{
  public class MetaWeblogHandler : XmlRpcService, IMetaWeblog
  {
    string IMetaWeblog.AddPost(string blogid, string username, string password, PostEntity post, bool publish)
    {
      //var request = System.Web.HttpContext.Current.Request;
      //request.InputStream.Position = 0;
      //var input = new StreamReader(request.InputStream).ReadToEnd();

      ValidateUser(username, password);

      post.Author = username;
      post.IsPublished = publish;
      PostService.Instance.Add(post.ToPost());

      return post.Id.ToString(CultureInfo.InvariantCulture);
    }

    bool IMetaWeblog.UpdatePost(string postid, string username, string password, PostEntity post, bool publish)
    {
      ValidateUser(username, password);

      var postService = PostService.Instance;
      var match = postService.Get(postid);
      if (match == null) return false;

      match.Title = post.Title;
      match.Content = post.Content;
      match.Keywords = post.Keywords;
      match.PublishDate = post.PublishDate;
      match.IsPublished = publish;
      postService.Edit(match);
      return true;
    }

    bool IMetaWeblog.DeletePost(string key, string postid, string username, string password, bool publish)
    {
      ValidateUser(username, password);

      var postService = PostService.Instance;
      var post = postService.Get(postid);
      if (post == null) return false;

      postService.Delete(post);
      return true;
    }

    object IMetaWeblog.GetPost(string postid, string username, string password)
    {
      ValidateUser(username, password);

      var post = PostService.Instance.Get(postid);
      if (post == null) throw new XmlRpcFaultException(0, "Post does not exist");

      return new
      {
        postid = post.Id,
        title = post.Title,
        description = post.Content,
        dateCreated = post.PublishDate,
        mt_keywords = post.Keywords ?? ""
      };
    }

    object[] IMetaWeblog.GetAllPosts(string username, string password)
    {
      ValidateUser(username, password);

      return PostService.Instance.Query
        .Where(p => p.Author == username)
        .ToArray()
        .Select(post => new
        {
          postid = post.Id,
          title = post.Title,
          description = post.Content,
          dateCreated = post.PublishDate,
          mt_keywords = post.Keywords ?? ""
        })
        .Cast<object>()
        .ToArray();
    }

    object[] IMetaWeblog.GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
      ValidateUser(username, password);

      return PostService.Instance.Query
        .Where(p => p.Author == username)
        .Take(numberOfPosts)
        .ToArray()
        .Select(post => new
        {
          postid = post.Id,
          title = post.Title,
          description = post.Content,
          dateCreated = post.PublishDate,
          mt_keywords = post.Keywords ?? ""
        })
        .Cast<object>()
        .ToArray();
    }

    object[] IMetaWeblog.GetCategories(string blogid, string username, string password)
    {
      ValidateUser(username, password);

      var list = new List<object>();
      var categories = PostService.Instance.Query.Select(p => p.Keywords).ToList().SelectMany(t => (t ?? "").Split(','));

      foreach (string category in categories.Distinct())
      {
        list.Add(new { title = category });
      }

      return list.ToArray();
    }

    object IMetaWeblog.NewMediaObject(string blogid, string username, string password, MediaObject media)
    {
      ValidateUser(username, password);

      var path = FileService.SaveFileToDisk(media.bits, Path.GetExtension(media.name));
      return new { url = path };
    }

    object[] IMetaWeblog.GetUsersBlogs(string key, string username, string password)
    {
      ValidateUser(username, password);

      return new[]
      {
        new
        {
          blogid = "1",
          blogName = "bLOG",
          url = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority
        }
      };
    }




    private static void ValidateUser(string username, string password)
    {
      if (!SecurityService.Authenticate(username, password))
      {
        throw new XmlRpcFaultException(0, "User is not valid!");
      }
    }
  }
}
