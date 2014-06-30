using CookComputing.XmlRpc;

namespace bLOG.Web.Framework.MetaWeblog
{
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public struct MediaObject
  {
    public string name;
    public string type;
    public byte[] bits;
  }
}