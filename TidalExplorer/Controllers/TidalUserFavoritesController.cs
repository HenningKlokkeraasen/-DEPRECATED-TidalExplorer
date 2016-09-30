using System.Threading.Tasks;
using System.Web.Mvc;
using TidalExplorer.TidalIntegration;

namespace TidalExplorer.Controllers
{
    public class TidalUserFavoritesController : Controller
    {
        [Authorize]
        public async Task<ActionResult> Artists()
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(User.Identity);

            if (session == null)
                return new EmptyResult();

            var favoriteArtists = await session.GetFavoriteArtists();

            if (favoriteArtists == null)
                return new EmptyResult();

            return View("Artists", favoriteArtists);
        }
        

        [Authorize]
        public async Task<ActionResult> Albums()
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(User.Identity);

            if (session == null)
                return new EmptyResult();

            var favoriteAlbums = await session.GetFavoriteAlbums();

            if (favoriteAlbums == null)
                return new EmptyResult();

            return View("Albums", favoriteAlbums);
        }
    }
}