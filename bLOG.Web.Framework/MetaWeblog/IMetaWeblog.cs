using bLOG.Web.Framework.MetaWeblog.Entities;
using CookComputing.XmlRpc;

namespace bLOG.Web.Framework.MetaWeblog
{
  public interface IMetaWeblog
  {
    #region MetaWeblog API

    [XmlRpcMethod("metaWeblog.newPost")]
    string AddPost(string blogid, string username, string password, PostEntity post, bool publish);

    [XmlRpcMethod("metaWeblog.editPost")]
    bool UpdatePost(string postid, string username, string password, PostEntity post, bool publish);

    [XmlRpcMethod("metaWeblog.getPost")]
    object GetPost(string postid, string username, string password);

    [XmlRpcMethod("metaWeblog.getCategories")]
    object[] GetCategories(string blogid, string username, string password);

    [XmlRpcMethod("metaWeblog.getRecentPosts")]
    object[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);

    [XmlRpcMethod("metaWeblog.newMediaObject")]
    object NewMediaObject(string blogid, string username, string password, MediaObject mediaObject);

    [XmlRpcMethod("metaWeblog.getAllPosts")]
    object[] GetAllPosts(string username, string password);

    #endregion

    #region Blogger API

    [XmlRpcMethod("blogger.deletePost")]
    [return: XmlRpcReturnValue(Description = "Returns true.")]
    bool DeletePost(string key, string postid, string username, string password, bool publish);

    [XmlRpcMethod("blogger.getUsersBlogs")]
    object[] GetUsersBlogs(string key, string username, string password);

    #endregion
  }
}