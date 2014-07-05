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
    protected DbSet<T> Set { get; private set; }

    protected DataService()
    {
      Set = Context.Set<T>();
    }

    public IQueryable<T> Query { get { return Set.AsNoTracking(); } }
    public IQueryable<T> QueryForEdit { get { return Set.AsQueryable(); } }


    public IEnumerable<T> Get()
    {
      return Set.AsNoTracking().ToList();
    }
    public IEnumerable<T> Get(Func<T, bool> predicate)
    {
      return Set.AsNoTracking().Where(predicate).ToList();
    }
    public IEnumerable<T> GetForEdit()
    {
      return Set.ToList();
    }
    public IEnumerable<T> GetForEdit(Func<T, bool> predicate)
    {
      return Set.Where(predicate).ToList();
    }

    public int Add(T entity)
    {
      Set.Add(entity);
      return Context.SaveChanges();
    }

    public int Edit(T entity)
    {
      Context.Entry(entity).State = EntityState.Modified;
      return Context.SaveChanges();
    }

    public int Delete(T entity)
    {
      Context.Entry(entity).State = EntityState.Deleted;
      return Context.SaveChanges();
    }

  }
}
