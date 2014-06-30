using System.Collections.Generic;
using System.Linq;
using System.Web;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Views
{
  public class BasicView : IView
  {
    private readonly string _virtualPath;
    private readonly Dictionary<string, object> _replacements = new Dictionary<string, object>();

    public BasicView(string virtualPath)
    {
      UseLayout = true;
      _virtualPath = virtualPath;
    }

    public void ResetTokens()
    {
      _replacements.Clear();
    }
    public void UpdateToken(string token, object value)
    {
      if (_replacements.ContainsKey(token))
      {
        _replacements[token] = value;
      }
      else
      {
        _replacements.Add(token, value);
      }
    }
    public string Render()
    {
      return _replacements.Aggregate(ViewContainer.GetContent(_virtualPath),
        (current, replacement) =>
          current.Replace(string.Format(WebConfig.ViewTokenFormat, replacement.Key), replacement.Value.ToString()));
    }

    public bool UseLayout { get; set; }

    public void Render(HttpContext context)
    {
      context.Response.Write(Render());
    }
  }
}
