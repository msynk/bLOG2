using System.Web.Routing;
using bLOG.Web.Framework;
using bLOG.Web.Framework.Routing;

namespace bLOG.Web.Framework.Startup
{
  class RoutingStartup
  {
    private static readonly RouteCollection Routes = RouteTable.Routes;
    public static void Start()
    {
      RegisterHandlers();
    }

    private static void RegisterHandlers()
    {
      Routes.Ignore("metaweblog");

      //Routes.MapGenericHandlerRoute("Global",
      //  string.Format("{{{0}}}/{{{1}}}/{{{2}}}/{{*pathInfo}}", WebConfig.HanlderRoute, WebConfig.ActionRoute, WebConfig.IdRoute),
      //  string.Format("~/Handlers/{{{0}}}Handler.ashx", WebConfig.HanlderRoute),
      //  new RouteValueDictionary { { WebConfig.HanlderRoute, "Home" }, { WebConfig.ActionRoute, "Index" }, { WebConfig.IdRoute, "1" } });

      Routes.MapHttpHandlerRoute("ShowPost",
        string.Format("post/{{{0}}}/{{*pathInfo}}", WebConfig.IdRoute),
        string.Format("bLOG.Web.Framework.Handlers.PostHandler"),
        new RouteValueDictionary { { WebConfig.HanlderRoute, "Post" }, { WebConfig.ActionRoute, "Index" }, { WebConfig.IdRoute, "1" } });

      Routes.MapHttpHandlerRoute("Global",
        string.Format("{{{0}}}/{{{1}}}/{{{2}}}/{{*pathInfo}}", WebConfig.HanlderRoute, WebConfig.ActionRoute, WebConfig.IdRoute),
        string.Format("bLOG.Web.Framework.Handlers.{{{0}}}Handler", WebConfig.HanlderRoute),
        new RouteValueDictionary { { WebConfig.HanlderRoute, "Home" }, { WebConfig.ActionRoute, "Index" }, { WebConfig.IdRoute, "1" } });
    }
  }
}
