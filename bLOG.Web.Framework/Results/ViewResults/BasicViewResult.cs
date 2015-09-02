using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
    public class BasicViewResult : IViewResult
    {
        private readonly string _virtualPath;
        private readonly Dictionary<string, object> _replacements = new Dictionary<string, object>();

        public BasicViewResult(string virtualPath)
        {
            _virtualPath = virtualPath;
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
            foreach (var item in _replacements)
            {
                layoutView.UpdateToken(item.Key, item.Value);
            }

            layoutView.UpdateToken(WebConfig.PageBodyToken, result);
            layoutView.UpdateToken(WebConfig.VersionToken, WebConfig.Version);
            layoutView.UpdateToken(WebConfig.BlogTitleToken, WebConfig.BlogTitle);
            layoutView.UpdateToken(WebConfig.CopyrightToken, WebConfig.Copyright);
            SetBlogLinks(layoutView);

            return layoutView.Render();
        }

        private void SetBlogLinks(IViewResult viewResult)
        {
            SetLinkTokens(WebConfig.UrlGithub, WebConfig.UrlGithubToken, WebConfig.DisplayGithubToken, viewResult);
            SetLinkTokens(WebConfig.UrlLinkedin, WebConfig.UrlLinkedinToken, WebConfig.DisplayLinkedinToken, viewResult);
            SetLinkTokens(WebConfig.UrlFacebook, WebConfig.UrlFacebookToken, WebConfig.DisplayFacebookToken, viewResult);
            SetLinkTokens(WebConfig.UrlTwitter, WebConfig.UrlTwitterToken, WebConfig.DisplayTwitterToken, viewResult);
            SetLinkTokens(WebConfig.UrlGoogle, WebConfig.UrlGoogleToken, WebConfig.DisplayGoogleToken, viewResult);
            SetLinkTokens(WebConfig.UrlInstagram, WebConfig.UrlInstagramToken, WebConfig.DisplayInstagramToken, viewResult);
        }
        private void SetLinkTokens(string url, string urlToken, string displayToken, IViewResult viewResult)
        {
            if (!string.IsNullOrEmpty(url))
            {
                viewResult.UpdateToken(urlToken, url);
                viewResult.UpdateToken(displayToken, "inline");
            }
            else
            {
                viewResult.UpdateToken(displayToken, "none");
            }
        }

    }
}
