using System.Linq;
using System.Reflection;
using System.Web;
using bLOG.Web.Framework.Views;

namespace bLOG.Web.Framework.Handlers
{
  public abstract class BaseHandler : IHttpHandler
  {
    public HttpContext Context { get; private set; }
    public HttpRequest Request { get { return Context == null ? null : Context.Request; } }
    public HttpResponse Response { get { return Context == null ? null : Context.Response; } }

    #region IHttpHandler

    public bool IsReusable { get { return false; } }

    public void ProcessRequest(HttpContext context)
    {
      Context = context;
      var view = ProcessRequestInternal() ?? ViewContainer.UnknownRequestView;
      if (view.UseLayout)
      {
        view = Layout(view);
      }
      view.Render(context);
    }

    #endregion

    protected virtual IView ProcessRequestInternal()
    {
      var type = GetType();
      var method = type.GetMethods().SingleOrDefault(m => m.Name.ToUpper() == Action.ToUpper());
      if (method != null)
      {
        //return method.Invoke(this, new object[] { Id }) as IView;
        return method.Invoke(this, null) as IView;
      }
      return null;
    }

    protected virtual string Title { get; set; }
    protected virtual string ViewsFolder { get { return Handler; } }
    protected string Handler { get { return Route(WebConfig.HanlderRoute).ToString(); } }
    protected string Action { get { return Route(WebConfig.ActionRoute).ToString(); } }
    protected string Id { get { return Route(WebConfig.IdRoute).ToString(); } }


    protected IView View()
    {
      return View(Action);
    }
    protected IView View(string viewName)
    {
      var view = ViewContainer.GetViewEngine(WebConfig.ViewPathProvider.GetPath(ViewsFolder, viewName));
      view.ResetTokens();
      return view;
    }

    protected object Route(string key)
    {
      return Request.RequestContext.RouteData.Values[key];
    }
    protected string QueryString(string name)
    {
      return Request.QueryString[name];
    }
    protected string Param(string name)
    {
      return Request.Params[name];
    }

    protected IView Layout(IView view)
    {
      var layout = ViewContainer.LayoutView;
      layout.UpdateToken(WebConfig.PageTitleToken, Title ?? "");
      layout.UpdateToken(WebConfig.PageBodyToken, view.Render());
      layout.UpdateToken(WebConfig.VersionToken, WebConfig.Version);

      return layout;
    }
  }
}
