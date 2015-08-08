using System.Web;

namespace bLOG.Web.Framework.Results
{
  public interface IHttpResult
  {
    void ExecuteResult(HttpContext context);
  }
}
