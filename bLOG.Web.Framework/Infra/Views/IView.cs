using System.Web;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Views
{
  public interface IView
  {
    bool UseLayout { get; set; }
    void Render(HttpContext context);
    string Render();

    void ResetTokens();
    void UpdateToken(string token, object value);
  }
}
