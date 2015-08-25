using System;
using System.Linq;
using System.Web;
using bLOG.Web.Framework.Results;
using bLOG.Web.Framework.Results.ViewResults;

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
            view.ExecuteResult(context);
        }

        #endregion

        protected virtual IHttpResult ProcessRequestInternal()
        {
            var type = GetType();
            var method = type.GetMethods().SingleOrDefault(m => m.Name.ToUpper() == Action.ToUpper());
            if (method == null)
            {
                return null;
            }
            return FinalizeView(method.Invoke(this, null) as IHttpResult);
        }

        protected virtual string PageTitle { get; set; }
        protected virtual string ViewsFolder { get { return Handler; } }
        protected string Handler { get { return Route(WebConfig.HanlderRoute).ToString(); } }
        protected string Action { get { return Route(WebConfig.ActionRoute).ToString(); } }
        protected string Id { get { return Route(WebConfig.IdRoute).ToString(); } }

        protected virtual IHttpResult FinalizeView(IHttpResult result)
        {
            IViewResult viewResult = result as IViewResult;
            if (viewResult != null)
            {
                viewResult.UpdateToken(WebConfig.PageTitleToken, PageTitle);
            }
            return result;
        }

        protected IViewResult View()
        {
            return View(Action);
        }
        protected IViewResult View(string viewName)
        {
            var view = ViewContainer.GetViewEngine(WebConfig.ViewPathProvider.GetPath(ViewsFolder, viewName));
            view.UseLayout = true;
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
    }
}
