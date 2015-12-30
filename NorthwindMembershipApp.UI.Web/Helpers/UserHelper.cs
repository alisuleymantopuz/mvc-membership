using NorthwindMembershipApp.UI.Web.Models.Account;
using NorthwindMembershipApp.UI.Web.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Helpers
{
    using System.Web.Caching;

    public class UserHelper
    {
        public NORTHWNDEntities NorthwindEntities { get; private set; }

        public UserHelper()
        {
            this.NorthwindEntities = new NORTHWNDEntities();
        }

        public User Get(string email)
        {
            return this.NorthwindEntities.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}