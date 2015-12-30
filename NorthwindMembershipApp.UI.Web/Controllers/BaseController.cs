using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindMembershipApp.UI.Web.Filters;

namespace NorthwindMembershipApp.UI.Web.Controllers
{
    using NorthwindMembershipApp.UI.Web.Helpers;
    using NorthwindMembershipApp.UI.Web.Models.Account;
    using NorthwindMembershipApp.UI.Web.Orm;

    [IsJsonRequest]
    [RoleBasedAuthorize]
    [ValidateAntiForgeryTokenWrapper(HttpVerbs.Post)]
    public class BaseController : Controller
    {

        public bool IsJsonRequest { get; set; }

        public AccesiblePagesHelper AccesiblePagesHelper { get; set; }

        public string InvokedControllerName { get; set; }

        public string InvokedActionName { get; set; }

        public WebPrincipal WebPrincipal { get; set; }

        public List<Page> AccesiblePages { get; set; }

        public BaseController()
        {
            this.AccesiblePagesHelper = new AccesiblePagesHelper();

            this.WebPrincipal = System.Web.HttpContext.Current.User as WebPrincipal;


            if (WebPrincipal != null)
            {
                var cachedData = this.AccesiblePagesHelper.Get(this.WebPrincipal);

                this.AccesiblePages = cachedData as List<Page>;
            }

            SetStaticViewBagMessages();
        }

        private void SetStaticViewBagMessages()
        {

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            InvokedActionName = requestContext.RouteData.Values["action"].ToString();
            InvokedControllerName = requestContext.RouteData.Values["controller"].ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.AccesiblePages = null;
            }

            base.Dispose(disposing);
        }

        protected virtual new WebPrincipal User
        {
            get
            {
                return System.Web.HttpContext.Current.User as WebPrincipal;
            }
        }

        public int CurrentUserId
        {
            get
            {
                return this.User.Id;
            }
        }

        public bool IsAuthorize
        {
            get
            {
                if (HttpContext.Request.IsAuthenticated && this.AccesiblePages != null)
                {
                    return true;
                }

                return false;
            }
        }
    }
}