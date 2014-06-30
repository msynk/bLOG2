using System.Web;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Views
{
  public class RedirectView : IView
  {
    private readonly string _redirectUrl;

    public RedirectView(string redirectUrl)
    {
      _redirectUrl = redirectUrl;
    }

    public string Render()
    {
      return "";
    }

    public bool UseLayout { get; set; }

    public void Render(HttpContext context)
    {
      context.Response.Redirect(_redirectUrl);
    }

    public void ResetTokens()
    {
      throw new System.NotImplementedException();
    }
    public void UpdateToken(string token, object value)
    {
      throw new System.NotImplementedException();
    }
  }
}
