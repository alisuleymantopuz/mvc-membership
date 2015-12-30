using NorthwindMembershipApp.UI.Web.Helpers;
using NorthwindMembershipApp.UI.Web.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NorthwindMembershipApp.UI.Web.Models.Account;
using System.Security.Principal;

namespace NorthwindMembershipApp.UI.Web.Filters
{

    public class RoleBasedAuthorizeAttribute : AuthorizeAttribute
    {
        public AccesiblePagesHelper AccesiblePagesHelper { get; private set; }

        public UserHelper UserHelper { get; private set; }

        public WebPrincipalHelper WebPrincipalHelper { get; private set; }

        public RoleBasedAuthorizeAttribute()
        {
            this.AccesiblePagesHelper = new AccesiblePagesHelper();

            this.UserHelper = new UserHelper();

            this.WebPrincipalHelper = new WebPrincipalHelper();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
            {
                return false;
            }

            string controller = RouteTable.Routes.GetRouteData(httpContext).Values["controller"].ToString();
            string action = RouteTable.Routes.GetRouteData(httpContext).Values["action"].ToString();

            List<Page> accesiblePages = null;

            WebPrincipal webPrincipal = this.WebPrincipalHelper.PopulateWebPrincipal(HttpContext.Current.User);

            if (webPrincipal != null)
            {
                var cacheData = this.AccesiblePagesHelper.Get(webPrincipal);

                if (cacheData != null)
                {
                    accesiblePages = (List<Page>)cacheData;
                }

            }

            return IsUserAuthorizedToAction(accesiblePages, controller, action);
        }



        private bool IsUserAuthorizedToAction(List<Page> accesiblePages, string controller, string action)
        {
            if (accesiblePages == null)
            {
                return false;
            }

            foreach (var accesiblePage in accesiblePages)
            {
                if (accesiblePage.Action == action && accesiblePage.Controller == controller)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                WebPrincipal webPrincipal = HttpContext.Current.User as WebPrincipal;
                if (webPrincipal != null)
                {
                    var cacheData = AccesiblePagesHelper.Get(webPrincipal);
                    if (cacheData == null)
                    {
                        base.HandleUnauthorizedRequest(filterContext);
                        return;
                    }
                }
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                                                {"action","GenericError"},  
                                                {"controller","Error"}
                });
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}