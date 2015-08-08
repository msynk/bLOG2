using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace bLOG.Web.Framework.Routing
{
  public class HttpHandlerRouteHandler : IRouteHandler
  {
    public string VirtualPath { get; private set; }

    public HttpHandlerRouteHandler(string virtualPath)
    {
      VirtualPath = virtualPath;
    }
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      var name = VirtualPath;
      var virtualPathData = new Route(VirtualPath, this).GetVirtualPath(requestContext, requestContext.RouteData.Values);
      if (virtualPathData != null)
      {
        name = virtualPathData.VirtualPath;
      }
      var length = name.IndexOf('?');
      if (length != -1)
      {
        name = name.Substring(0, length);
      }

      var type = Type.GetType(name);
      if (type == null)
      {
        var assembly = Assembly.GetExecutingAssembly();
        type = assembly.GetTypes().SingleOrDefault(t => t.FullName.ToUpper() == name.ToUpper());
        if (type == null) return null;
      }
      
      return Activator.CreateInstance(type) as IHttpHandler;
    }
  }
}
