using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace DGRF.Common
{
    public class FormsPrincipal<TUserData> : IPrincipal
        where TUserData : class
    {
        private IIdentity identity;
        private TUserData userData;

        public FormsPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");

            this.identity = new FormsIdentity(ticket);
            this.userData = userData;
        }

        public TUserData UserData
        {
            get { return this.userData; }
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string roles)
        {
            IPrincipal principal = this.userData as IPrincipal;
            if (principal == null)
                throw new NotImplementedException();
            else
                return principal.IsInRole(roles);
        }

        public static void SignIn(string loginName, TUserData userData)
        {
            if (string.IsNullOrEmpty(loginName))
                throw new ArgumentNullException("loginName");
            if (userData == null)
                throw new ArgumentNullException("userData");

            string data = SerializeHelper.ObjectToString<TUserData>(userData);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                loginName,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                data
                );
            string cookieValue = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = ticket.Expiration;

            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
        }

        public static void SetLoginUserInfo(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpCookie cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return;

            try
            {
                TUserData userData = null;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                    userData = SerializeHelper.StringToObject<TUserData>(ticket.UserData);

                if (ticket != null && userData != null)
                    context.User = new FormsPrincipal<TUserData>(ticket, userData);
            }
            catch (Exception e)
            {
                throw new Exception("Failed, Error: " + e.Message);
            }
        }

    }
}
