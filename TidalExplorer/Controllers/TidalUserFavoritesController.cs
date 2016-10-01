using System.Threading.Tasks;
using System.Web.Mvc;

namespace TidalExplorer.Controllers
{
    public class TidalUserFavoritesController : BaseTidalController
    {
        [Authorize]
        public async Task<ActionResult> Artists()
        {
            return await ViewWithDataOrEmptyResult("Artists", session => session.GetFavoriteArtists());
        }

        [Authorize]
        public async Task<ActionResult> Albums()
        {
            return await ViewWithDataOrEmptyResult("Albums", session => session.GetFavoriteAlbums());
        }

        [Authorize]
        public async Task<ActionResult> Tracks()
        {
            return await ViewWithDataOrEmptyResult("Tracks", session => session.GetFavoriteTracks());
        }

        [Authorize]
        public async Task<ActionResult> Playlists()
        {
            return await ViewWithDataOrEmptyResult("Playlists", session => session.GetFavoritePlaylists());
        }
    }
}