using System;

namespace bLOG.Core.Domain
{
  public class Post
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public string Keywords { get; set; }
    public bool IsPublished { get; set; }
    public int ViewsCount { get; set; }
  }
}
