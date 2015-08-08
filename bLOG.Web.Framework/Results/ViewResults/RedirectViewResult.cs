using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
  public class RedirectViewResult : BasicViewResult
  {
    private readonly string _redirectUrl;

    public RedirectViewResult(string redirectUrl) : base(redirectUrl)
    {
      _redirectUrl = redirectUrl;
    }

    public override string Render()
    {
      return "";
    }

  }
}
