using NorthwindMembershipApp.UI.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindMembershipApp.UI.Web.Helpers;
using NorthwindMembershipApp.UI.Web.Orm;
using System.Web.Script.Serialization;

namespace NorthwindMembershipApp.UI.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public NORTHWNDEntities NorthwindEntities { get; private set; }

        public FormsAuthenticationService FormsAuthenticationService { get; private set; }
        public AccountController()
        {
            this.NorthwindEntities = new NORTHWNDEntities();
            this.FormsAuthenticationService = new FormsAuthenticationService();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user =
                    this.NorthwindEntities.Users.FirstOrDefault(
                        x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password);

                if (user != null)
                {
                    var userData = this.UserToWebPrincipalSerialization(user);

                    FormsAuthenticationService.SignIn(user.Email, userData, false);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    return this.Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login user is not exists..");
                    return this.View(loginViewModel);
                }
            }
            else
                ModelState.AddModelError("", "Login failed..");

            return View(loginViewModel);
        }

        public string UserToWebPrincipalSerialization(User user)
        {
            WebPrincipalSerializeModel webPrincipalSerializeModel =
                new WebPrincipalSerializeModel()
               {
                   Email = user.Email,
                   Id = user.Id,
                   Name = user.Name,
                   RoleId = user.RoleId.Value,
                   Surname = user.Surname

               };

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(webPrincipalSerializeModel);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthenticationService.SignOut();

            return RedirectToAction("Login");
        }
    }
}