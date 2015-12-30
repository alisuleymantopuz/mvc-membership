using NorthwindMembershipApp.UI.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindMembershipApp.UI.Web.Filters
{
    public class IsJsonRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var myController = filterContext.Controller as BaseController;
            if (myController != null)
            {
                if (filterContext.HttpContext.Request.AcceptTypes != null
                    && filterContext.HttpContext.Request.AcceptTypes.Contains("application/json"))
                {
                    myController.IsJsonRequest = true;
                }
                else
                {
                    myController.IsJsonRequest = false;
                }
            }
        }
    }
}