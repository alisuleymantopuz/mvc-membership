using NorthwindMembershipApp.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Models.Account
{
    [Serializable]
    public class WebPrincipal : IWebPrincipal
    {
        public RoleHelper RoleHelper { get; private set; }

        public WebPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
            this.RoleHelper = new RoleHelper();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return this.RoleHelper.IsInRole(this.Email, role);
        }
    }
}