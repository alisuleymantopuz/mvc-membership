﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindMembershipApp.UI.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult GenericError()
        {
            return View();
        }
    }
}