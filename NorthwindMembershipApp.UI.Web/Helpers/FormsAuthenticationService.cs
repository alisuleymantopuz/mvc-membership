using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NorthwindMembershipApp.UI.Web.Helpers
{
    public class FormsAuthenticationService
    {
        public static string FormsCookieName
        {
            get
            {
                return FormsAuthentication.FormsCookieName;
            }
        }

        public static FormsAuthenticationTicket Decrypt(string authCookie)
        {
            return FormsAuthentication.Decrypt(authCookie);
        }

        public void SignIn(string email, string userData, bool createPersistentCookie)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("required.", "Email");
            }

            int timeOut = 3600;

            var expirationTime = DateTime.Now.AddMinutes(timeOut);

            if (createPersistentCookie)
            {
                expirationTime = DateTime.Now.AddDays(2);
            }

            var ticket = new FormsAuthenticationTicket(
                1,
                email,
                DateTime.Now,
                expirationTime,
                createPersistentCookie,
                userData,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie("Account", encryptedTicket);
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);

            FormsAuthentication.SetAuthCookie(email, createPersistentCookie);

        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();

            HttpCookie accountCookie = new HttpCookie("Account", "");
            accountCookie.Expires = DateTime.Now.AddYears(-1);
            accountCookie.Path = FormsAuthentication.FormsCookiePath;
            accountCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(accountCookie);

            HttpCookie aspNetSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            aspNetSessionCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(aspNetSessionCookie);
        }

        public void RefreshAuthentication(HttpCookie authCookie, bool createPersistentCookie)
        {
            FormsAuthenticationTicket authTicket = FormsAuthenticationService.Decrypt(authCookie.Value);
            string email = authTicket.Name;
            string userData = authTicket.UserData;
            SignIn(email, userData, createPersistentCookie);
        }
    }
}