using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilities
{
    public class CookiesManager
    {
        public void Add(HttpContext context, string Token, string Value)
        {
            context.Response.Cookies.Append(Token, Value, GetCookieOption(context));
        }

        public CookieOptions GetCookieOption(HttpContext context)
        {
            return new()
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(1000)
            };
        }

        public bool Contain(HttpContext context, string Token)
        {
            return context.Request.Cookies.ContainsKey(Token);
        }

        public string GetValue(HttpContext context, string Token)
        {
            string CookieValue;
            if (!context.Request.Cookies.TryGetValue(Token, out CookieValue))
            {
                return null;
            }

            return CookieValue;
        }

        public void Remove(HttpContext context, string Token)
        {
            if (Contain(context, Token))
            {
                context.Response.Cookies.Delete(Token);
            }
        }

        public Guid GetBrowserId(HttpContext context)
        {
            string browseID = GetValue(context, "BrowserId");
            
            if (browseID == null)
            {
                string Value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", Value);
                browseID = Value;
            }

            Guid GuidBrowser;
            Guid.TryParse(browseID, out GuidBrowser);

            return GuidBrowser;
        }
    }
}
