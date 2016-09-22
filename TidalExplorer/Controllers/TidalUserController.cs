using System.Threading.Tasks;
using System.Web.Mvc;
using TidalExplorer.TidalIntegration;
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

        [Authorize]
        public async Task<ActionResult> UserPlaylists()
        {
            var session = OpenTidlIntegrator.RecreateSessionFromClaimsIdentity(User.Identity);

            if (session == null)
                return new EmptyResult();

            var userPlaylists = await session.GetUserPlaylists();

            if (userPlaylists == null)
                return new EmptyResult();

            return  View("Playlists", userPlaylists);
        }
    }
}