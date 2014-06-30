using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Routing
{
  public class GenericHandlerRouteHandler : IRouteHandler
  {
    private readonly bool _useRouteVirtualPath;
    private Route _routeVirtualPath;

    public string VirtualPath { get; private set; }

    public bool CheckPhysicalUrlAccess { get; private set; }

    private Route RouteVirtualPath
    {
      get { return _routeVirtualPath ?? (_routeVirtualPath = new Route(VirtualPath.Substring(2), this)); }
    }

    public GenericHandlerRouteHandler(string virtualPath)
    {
      VirtualPath = virtualPath;
      _useRouteVirtualPath = VirtualPath.Contains("{");
    }

    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (requestContext == null) throw new ArgumentNullException("requestContext");

      var virtualPath = GetSubstitutedVirtualPath(requestContext);
      var length = virtualPath.IndexOf('?');
      if (length != -1)
      {
        virtualPath = virtualPath.Substring(0, length);
      }
      return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(IHttpHandler)) as IHttpHandler;
    }

    public string GetSubstitutedVirtualPath(RequestContext requestContext)
    {
      if (requestContext == null)
        throw new ArgumentNullException("requestContext");

      if (!_useRouteVirtualPath)
      {
        return VirtualPath;
      }
      var pathData = RouteVirtualPath.GetVirtualPath(requestContext, requestContext.RouteData.Values);
      if (pathData == null)
      {
        return VirtualPath;
      }
      return "~/" + pathData.VirtualPath;
    }

  }
}
