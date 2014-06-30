using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bLOG.Data.Services
{
  public abstract class DataService<T> : BaseDataService where T : class
  {
    protected static readonly DbSet<T> Set = Context.Set<T>();

    protected DataService() { }

    public static IQueryable<T> Query { get { return Set.AsNoTracking(); } }
    public static IQueryable<T> QueryForEdit { get { return Set.AsQueryable(); } }


    public static IEnumerable<T> Get()
    {
      return Set.AsNoTracking().ToList();
    }
    public static IEnumerable<T> Get(Func<T, bool> predicate)
    {
      return Set.AsNoTracking().Where(predicate).ToList();
    }
    public static IEnumerable<T> GetForEdit()
    {
      return Set.ToList();
    }
    public static IEnumerable<T> GetForEdit(Func<T, bool> predicate)
    {
      return Set.Where(predicate).ToList();
    }

    public static int Add(T entity)
    {
      Set.Add(entity);
      return Context.SaveChanges();
    }

    public static int Edit(T entity)
    {
      Context.Entry(entity).State = EntityState.Modified;
      return Context.SaveChanges();
    }

    public static int Delete(T entity)
    {
      Context.Entry(entity).State = EntityState.Deleted;
      return Context.SaveChanges();
    }

  }
}
