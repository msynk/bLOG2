using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
  public class BasicViewResult : IViewResult
  {
    private readonly string _virtualPath;
    private readonly string _title = "";
    private readonly Dictionary<string, object> _replacements = new Dictionary<string, object>();

    public BasicViewResult(string virtualPath)
    {
      UseLayout = true;
      _virtualPath = virtualPath;
    }

    public BasicViewResult(string virtualPath, string title) : this(virtualPath)
    {
      _title = title;
    }

    public bool UseLayout { get; set; }


    public void ExecuteResult(HttpContext context)
    {
      context.Response.Write(Render());
    }

    public virtual string Render()
    {
      var result = _replacements.Aggregate(ViewContainer.GetContent(_virtualPath),
        (current, replacement) =>
          current.Replace(string.Format(WebConfig.ViewTokenFormat, replacement.Key), replacement.Value.ToString()));

      return UseLayout ? RenderLayout(result) : result;
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


    private string RenderLayout(string result)
    {
      var layoutView = ViewContainer.LayoutView;
      layoutView.UseLayout = false;
      layoutView.UpdateToken(WebConfig.PageTitleToken, _title);
      layoutView.UpdateToken(WebConfig.PageBodyToken, result);
      layoutView.UpdateToken(WebConfig.VersionToken, WebConfig.Version);

      return layoutView.Render();
    }

  }
}
