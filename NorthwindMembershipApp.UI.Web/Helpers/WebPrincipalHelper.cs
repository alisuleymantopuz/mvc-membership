using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Helpers
{
    using System.Security.Principal;

    using NorthwindMembershipApp.UI.Web.Models.Account;
    using NorthwindMembershipApp.UI.Web.Orm;

    public class WebPrincipalHelper
    {
        public UserHelper UserHelper { get; private set; }
        public WebPrincipalHelper()
        {
            this.UserHelper = new UserHelper();
        }

        public WebPrincipal PopulateWebPrincipal(IPrincipal principal)
        {
            if (principal != null && principal.Identity != null)
            {
                User user = this.UserHelper.Get(principal.Identity.Name);
                if (user != null)
                {
                    WebPrincipal webPrincipal = new WebPrincipal(user.Email)
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Name = user.Name,
                        RoleId = user.RoleId.Value,
                        Surname = user.Surname
                    };

                    return webPrincipal;
                }
            }
            return null;
        }
    }
}