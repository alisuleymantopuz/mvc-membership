using NorthwindMembershipApp.UI.Web.Models.Account;
using NorthwindMembershipApp.UI.Web.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Helpers
{
    using System.Web.Caching;

    public class AccesiblePagesHelper
    {
        public NORTHWNDEntities NorthwindEntities { get; private set; }
        
        public AccesiblePagesHelper()
        {
            this.NorthwindEntities = new NORTHWNDEntities();
        }

        public object Get(WebPrincipal webPrincipal)
        {
            string cacheKey = string.Format("accesible_pages_for_role_{0}", webPrincipal.RoleId);

            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                var role = this.NorthwindEntities.Roles.Find(webPrincipal.RoleId);

                if (role != null)
                {
                    var allAccesiblePages = role.Pages.ToList();

                    HttpContext.Current.Cache.Add(
                        cacheKey,
                        allAccesiblePages,
                        null,
                        DateTime.Now.AddMinutes(1),
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        CacheItemPriority.Default,
                        null);
                }

            }

            return (List<Page>)HttpContext.Current.Cache[cacheKey];
        }
    }
}