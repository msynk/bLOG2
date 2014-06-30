using System;
using System.Data.Entity;
using bLOG.Core.Domain;
using bLOG.Data.Mappings;

namespace bLOG.Data.Contexts
{
  public partial class bLOGContext : DbContext
  {
    static bLOGContext()
    {
      Database.SetInitializer<bLOGContext>(null);
    }

    public bLOGContext()
      : base("Name=bLOGContext")
    {
      Configuration.LazyLoadingEnabled = false;
      Configuration.ProxyCreationEnabled = false;
    }

    public override int SaveChanges()
    {
      try
      {
        return base.SaveChanges();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new PostMap());
    }
  }
}
