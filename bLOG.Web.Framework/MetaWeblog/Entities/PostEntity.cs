using System;
using System.Linq;
using bLOG.Core.Domain;
using CookComputing.XmlRpc;

namespace bLOG.Web.Framework.MetaWeblog.Entities
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public class PostEntity
  {
    public PostEntity()
    {
      Id = Guid.NewGuid().ToString();
      Author = "New author";
      Title = "My new post";
      Content = "the content";
      PublishDate = DateTime.UtcNow;
      Categories = new string[0];
      Keywords = "keywords";
      IsPublished = true;
      ViewsCount = 0;
    }

    public PostEntity(Post post)
    {
      Id = post.Id;
      Title = post.Title;
      Author = post.Author;
      Content = post.Content;
      PublishDate = post.PublishDate;
      Keywords = Keywords;
      IsPublished = post.IsPublished;
      ViewsCount = post.ViewsCount;
    }


    [XmlRpcMember("postid")]
    public string Id { get; set; }

    [XmlRpcMember("title")]
    public string Title { get; set; }

    [XmlRpcMember("author")]
    public string Author { get; set; }

    [XmlRpcMember("description")]
    public string Content { get; set; }

    [XmlRpcMember("dateCreated")]
    public DateTime PublishDate { get; set; }

    [XmlRpcMember("dateModified")]
    public DateTime LastModifiedDate { get; set; }

    [XmlRpcMember("categories")]
    public string[] Categories { get; set; }

    [XmlRpcMember("wp_slug")]
    public string Slug { get; set; }

    [XmlRpcMember("mt_excerpt")]
    public string Excerpt { get; set; }


    public bool IsPublished { get; set; }
    public int ViewsCount { get; set; }

    [XmlRpcMember("mt_keywords")]
    public string Keywords { get; set; }


    public Post ToPost()
    {
      return new Post
      {
        Id = Id,
        Title = Title,
        Author = Author,
        Content = Content,
        PublishDate = PublishDate,
        Keywords = Keywords,
        IsPublished = IsPublished,
        ViewsCount = ViewsCount,
      };
    }
  }
}
