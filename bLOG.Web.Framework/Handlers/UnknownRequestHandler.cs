using bLOG.Web.Framework.Views;

namespace bLOG.Web.Framework.Handlers
{
  public class UnknownRequestHandler : BaseHandler
  {
    protected override IView ProcessRequestInternal()
    {
      return null;
    }
  }
}