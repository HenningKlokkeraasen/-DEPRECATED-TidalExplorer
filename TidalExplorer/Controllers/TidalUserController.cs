using System.Web.Mvc;
using TidalExplorer.TidalIntegration.AspNetIdentity;

namespace TidalExplorer.Controllers
{
    [Authorize]
    public class TidalUserController : Controller
    {
        public ActionResult UserProfile()
        {
            var deserializeUserModel = TidalClaimsDeserializer.DeserializeUserModel(User.Identity);

            if (deserializeUserModel == null)
                return new EmptyResult();

            return PartialView("Partials/UserProfile", deserializeUserModel);
        }
    }
}