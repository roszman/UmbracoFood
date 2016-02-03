using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Controllers.Surface
{
    public class AccountSurfaceController : SurfaceController
    {
        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return new RedirectResult(returnUrl);
                }

                return RedirectToUmbracoPage(1056);
            }

            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");

            return CurrentUmbracoPage();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToUmbracoPage(1056);
        }
    }
}