using bLOG.Web.Framework.Results;

namespace bLOG.Web.Framework.Handlers
{
  public class UnknownRequestHandler : BaseHandler
  {
    protected override IHttpResult ProcessRequestInternal()
    {
      return null;
    }
  }
}