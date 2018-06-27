using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ERISCOTools.Security
{
    public class AuthCookie
    {
        private string COOKIE_NAME { get; set; }

        public AuthCookie(string COOKIE_NAME)
        {
            this.COOKIE_NAME = COOKIE_NAME;
        }

        public void create(string username)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddYears(100), true, username, FormsAuthentication.FormsCookiePath);
            string encryptedIdentityTicket = FormsAuthentication.Encrypt(ticket);
            var identityCookie = new HttpCookie(COOKIE_NAME, encryptedIdentityTicket);
            identityCookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(identityCookie);

            FormsAuthentication.SetAuthCookie(username, true);
        }

        public string get()
        {
            if (System.Web.HttpContext.Current.Request.Cookies[COOKIE_NAME] != null)
            {
                var cookies = System.Web.HttpContext.Current.Request.Cookies[COOKIE_NAME];

                if (cookies == null || string.IsNullOrEmpty(cookies.Value))
                {
                    return "";
                }
                try
                {
                    var name = FormsAuthentication.Decrypt(cookies.Value).Name;
                    return name == null ? "" : name;
                }
                catch
                {
                    return "";
                }
            }
            return "";
        }

        public bool isExpired()
        {
            if (HttpContext.Current.Request.Cookies[COOKIE_NAME] != null)
            {
                var cookies = HttpContext.Current.Request.Cookies[COOKIE_NAME];
                var cookie = FormsAuthentication.Decrypt(cookies.Value);
                if (cookie != null)
                {
                    return cookie.Expired;
                }
            }
            return true;
        }

        public void destroy()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var cookieName = cookie.Name;
                        var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); 
                    }
                }
                HttpContext.Current.Request.Cookies.Clear();
            }
            FormsAuthentication.SignOut();
        }

    }
}
