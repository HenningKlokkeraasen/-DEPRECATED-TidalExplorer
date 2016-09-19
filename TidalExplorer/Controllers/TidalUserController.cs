using System.Web.Mvc;
using TidalExplorer.TidalIntegration.AspNetIdentity;

namespace TidalExplorer.Controllers
{
    [Authorize]
    public class TidalUserController : Controller
    {
        public ActionResult TidalProfile()
        {
            var deserializeUserModel = TidalClaimsDeserializer.DeserializeUserModel(User.Identity);
            return deserializeUserModel == null
                ? (ActionResult) new EmptyResult()
                : PartialView("Partials/TidalProfile", deserializeUserModel);
        }
    }
}