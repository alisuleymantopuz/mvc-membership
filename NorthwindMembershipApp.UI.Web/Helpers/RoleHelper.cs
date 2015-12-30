using NorthwindMembershipApp.UI.Web.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Helpers
{
    public class RoleHelper
    {
        public NORTHWNDEntities NorthwindEntities { get; private set; }

        public RoleHelper()
        {
            this.NorthwindEntities = new NORTHWNDEntities();
        }

        public bool IsInRole(string email, string roleName)
        {
            var user = this.NorthwindEntities.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if (user.Role.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}