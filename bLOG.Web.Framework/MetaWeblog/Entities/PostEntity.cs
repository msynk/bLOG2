using System;
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
      IsPublished = true;
      ViewsCount = 0;
    }


    [XmlRpcMember("postid")]
    public string Id { get; set; }

    [XmlRpcMember("title")]
    public string Title { get; set; }

    [XmlRpcMember("dateCreated")]
    public DateTime PublishDate { get; set; }

    [XmlRpcMember("description")]
    public string Content { get; set; }


    public bool IsPublished { get; set; }
    public int ViewsCount { get; set; }
    public string Author { get; set; }


    public Post ToPost()
    {
      return new Post
      {
        Id = Id,
        Title = Title,
        Content = Content,
        PublishDate = PublishDate,
        IsPublished = IsPublished,
        ViewsCount = ViewsCount,
        Author = Author
      };
    }
  }
}
