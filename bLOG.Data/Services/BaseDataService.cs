using bLOG.Data.Contexts;

namespace bLOG.Data.Services
{
  public abstract class BaseDataService
  {
    protected bLOGContext Context { get; private set; }

    protected BaseDataService()
    {
      Context = new bLOGContext();
    }
  }
}
