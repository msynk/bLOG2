using System.Collections.Generic;
using System.Linq;
using bLOG.Core.Domain;

namespace bLOG.Data.Services
{
  public class PostService : DataService<Post>
  {
    //public static PostService Instance { get { return new PostService(); } }

    public static Post Get(string id)
    {
      return Set.AsNoTracking().SingleOrDefault(p => p.Id == id);
    }

    public static IEnumerable<Post> GetByAuthor(string author)
    {
      return Set.AsNoTracking().Where(p => p.Author == author).ToList();
    }

    public static IEnumerable<Post> Take(int count)
    {
      return Set.AsNoTracking().Take(count).ToList();
    }
  }
}
