using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using OpenTidl.Methods;
using OpenTidl.Models;
using TidalExplorer.Models;
using TidalExplorer.TidalIntegration;
using TidalExplorer.TidalIntegration.AspNetIdentity;

namespace TidalExplorer.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Based on http://stackoverflow.com/questions/32880269/how-to-do-session-management-in-aspnet-identity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var session = await TidalUserManager.LoginToTidal(model.Username, model.Password);

            if (session == null)
            {
                ModelState.AddModelError("", "invalid Tidal username or password");
                return View();
            }

            var userModel = await session.GetUser();
            var claimsIdentity = BuildClaimsIdentity(model, session, userModel);

            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = model.RememberMe}, claimsIdentity);

            return ViewBag.ReturnUrl != null
                ? Redirect(ViewBag.ReturnUrl)
                : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOut()
        {
            await TidalUserManager.LogOutFromTidal(User.Identity);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private static ClaimsIdentity BuildClaimsIdentity(LoginViewModel model, OpenTidlSession session, UserModel userModel)
        {
            var claims = new[]
            {
                // Claims to support default antiforgery provider
                new Claim(ClaimTypes.NameIdentifier, model.Username),
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                // Name
                new Claim(ClaimTypes.Name, model.Username),

                // Custom claims for Tidal session
                new Claim(TidalClaimTypes.TidalLoginModelSerialized, JsonConvert.SerializeObject(session.LoginResult)),
                new Claim(TidalClaimTypes.TidalUserModelSerialized, JsonConvert.SerializeObject(userModel))
            };
            return new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}